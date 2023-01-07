using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Enum;
using WebCoreAPI.Migrations;
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
                var accessToken = GenerateToken(user);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddSeconds(60);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    accessToken = accessToken,
                    refreshToken = refreshToken
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

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                throw new Exception("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new Exception("Invalid access token or refresh token");
            }
            //var user = _dbContext.Repository<AppUser>().SingleOrDefault(u => u.RefreshToken == refreshToken);
            string username = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            var newAccessToken = GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }

        private string GenerateToken(AppUser user)
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
                expires: DateTime.UtcNow.AddSeconds(10));

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }

        private string GenerateRefreshToken()
        {
            var refreshToken = getUniqueToken();

            return refreshToken;

            string getUniqueToken()
            {
                // token is a cryptographically strong random sequence of values
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                // ensure token is unique by checking against db
                var tokenIsUnique = !_dbContext.Repository<AppUser>().Any(u => u.RefreshToken == token);

                if (!tokenIsUnique)
                    return getUniqueToken();

                return token;
            }
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer =_appSettings.Issuer,
                ValidAudience = _appSettings.Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.SecretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
