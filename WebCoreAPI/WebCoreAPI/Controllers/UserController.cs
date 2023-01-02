using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Enum;
using WebCoreAPI.Models;
using WebCoreAPI.Models.Auth;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        //private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public readonly IPasswordHasher<AppUser> _passwordHasher;
        public readonly IGenericDbContext<AppDbContext> _dbContext;
        private readonly IHttpContextCurrentUser _httpContextCurrentUser;

        public UserController(IOptionsMonitor<AppSettings> optionsMonitor,
            //IUserService userService,
            UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHasher,
            IGenericDbContext<AppDbContext> dbContext,
            IHttpContextCurrentUser httpContextCurrentUser)
        {
            _appSettings = optionsMonitor.CurrentValue;
            //_userService = userService;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _httpContextCurrentUser = httpContextCurrentUser;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        [Authorize(Policy = "Role")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user is null)
                user = await _userManager.FindByEmailAsync(input.UserName);

            if (user is null)
                return Unauthorized();
            if (await _userManager.CheckPasswordAsync(user, input.Password))
            {
                var tokenModel = await GenerateToken(user);
                return Ok(new
                {
                    tokenModel.AccessToken,
                    tokenModel.RefreshToken,
                    user.IsFirstTimeLogin
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(UserCreateOrUpdateDto input)
        {
            var user = new AppUser
            {
                UserName = input.UserName ?? input.Email,
                FullName = input.FullName,
                Email = input.Email,
                IsFirstTimeLogin = true,
                UseType = UserType.SuperAdmin,
                //EmployeeCode = input.UserName,
                //Password = input.Password
            };

            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        [Route("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = _httpContextCurrentUser.UserId;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var permissions = (from s in _dbContext.Repository<AppUserRole>().AsNoTracking()
                                   join sa in _dbContext.Repository<IdentityRoleClaim<int>>().AsNoTracking() on s.RoleId equals sa.RoleId
                                   where s.UserId == user.Id && sa.ClaimType == "Permission" //Permissions.Type
                                   select sa.ClaimValue)
                            .ToList();

                var roleName = (from s in _dbContext.Repository<AppUserRole>()
                                join sa in _dbContext.Repository<AppRole>() on s.RoleId equals sa.Id
                                where s.UserId == user.Id
                                select sa.Name).FirstOrDefault();
                return Ok(new
                {
                    user.Email,
                    user.FullName,
                    roleName,
                    permissions,
                    user.IsFirstTimeLogin
                });
            }
            return Unauthorized();
        }

        private async Task<TokenModel> GenerateToken(AppUser user)
        {
            var roleName = (from s in _dbContext.Repository<AppUserRole>()
                            join sa in _dbContext.Repository<AppRole>() on s.RoleId equals sa.Id
                            where s.UserId == user.Id
                            select sa.Name).FirstOrDefault();

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var issuer = _appSettings.Issuer;
            var audience = _appSettings.Audience;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>//(permissions.Select(p => new Claim(Permissions.Type, p)))
                    {
                        new Claim(DefineClaimTypes.UserId, user.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email??user.UserName),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role, roleName)
                    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(30));

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Lưu database
            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };

            await _dbContext.Repository<RefreshToken>().AddAsync(refreshTokenEntity);
            await _dbContext.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var issuer = _appSettings.Issuer;
            var audience = _appSettings.Audience;

            var tokenValidateParam = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };

            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)//false
                    {
                        return Ok(new ApiResponse<object>
                        {
                            Status = false,
                            Message = "Invalid token"
                        });
                    }
                }

                //check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return Ok(new ApiResponse<object>
                    {
                        Status = false,
                        Message = "Access token has not yet expired"
                    });
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _dbContext.Repository<RefreshToken>().FirstOrDefault(x => x.Token == model.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new ApiResponse<object>
                    {
                        Status = false,
                        Message = "Refresh token does not exist"
                    });
                }

                //check 5: check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return Ok(new ApiResponse<object>
                    {
                        Status = false,
                        Message = "Refresh token has been used"
                    });
                }
                if (storedToken.IsRevoked)
                {
                    return Ok(new ApiResponse<object>
                    {
                        Status = false,
                        Message = "Refresh token has been revoked"
                    });
                }

                ////check 6: AccessToken id == JwtId in RefreshToken
                //var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                //if (storedToken.JwtId != jti)
                //{
                //    return Ok(new ApiResponse<object>
                //    {
                //        Status = false,
                //        Message = "Token doesn't match"
                //    });
                //}

                //Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _dbContext.Repository<RefreshToken>().Update(storedToken);
                await _dbContext.SaveChangesAsync();

                //create new token
                var user = await _dbContext.Repository<AppUser>().SingleOrDefaultAsync(user => user.Id == storedToken.UserId);
                var token = await GenerateToken(user);

                return Ok(new ApiResponse<object>
                {
                    Status = true,
                    Message = "Refresh token success",
                    Data = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Status = false,
                    Message = "Something went wrong"
                });
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
}
