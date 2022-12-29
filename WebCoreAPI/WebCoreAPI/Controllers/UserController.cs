using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Enum;
using WebCoreAPI.Models;
using WebCoreAPI.Models.Auth;
using WebCoreAPI.Models.Permission;

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

        public UserController(IOptionsMonitor<AppSettings> optionsMonitor,
            //IUserService userService,
            UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHasher,
            IGenericDbContext<AppDbContext> dbContext)
        {
            _appSettings = optionsMonitor.CurrentValue;
            //_userService = userService;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
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
                var permissions = (from s in _dbContext.Repository<AppUserRole>().AsNoTracking()
                                   join sa in _dbContext.Repository<IdentityRoleClaim<int>>().AsNoTracking() on s.RoleId equals sa.RoleId
                                   where s.UserId == user.Id && sa.ClaimType == "Permission" //Permissions.Type
                                   select sa.ClaimValue)
                            .ToList();

                var roleName = (from s in _dbContext.Repository<AppUserRole>()
                                join sa in _dbContext.Repository<AppRole>() on s.RoleId equals sa.Id
                                where s.UserId == user.Id
                                select sa.Name).FirstOrDefault();

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
                    expires: DateTime.UtcNow.AddHours(24));

                var tokenHandler = new JwtSecurityTokenHandler();
                var accessToken = tokenHandler.WriteToken(token);
                return Ok(new
                {
                    accessToken,
                    user.Email,
                    user.FullName,
                    roleName,
                    permissions,
                    user.IsFirstTimeLogin
                });
            }

            return Ok(user);
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
            return Ok("Profile");
        }
    }
}
