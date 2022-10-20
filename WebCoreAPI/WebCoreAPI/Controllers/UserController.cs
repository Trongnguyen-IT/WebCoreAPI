using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Entity;
using WebCoreAPI.Enum;
using WebCoreAPI.Migrations;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserController(IUserService userService,
            UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHasher)
        {
            _userService = userService;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user is null)
                user = await _userManager.FindByEmailAsync(input.UserName);

            if (user is null)
                return Unauthorized();
            if (await _userManager.CheckPasswordAsync(user, input.Password))
            {
                //var query = from s in db.UserRoles.AsNoTracking()
                //            join sa in db.RoleClaims.AsNoTracking() on s.RoleId equals sa.RoleId
                //            where s.UserId == user.Id && sa.ClaimType == "Permission" //Permissions.Type
                //            select sa.ClaimValue;

                //var permissions = query.ToList();

                //query = from s in db.UserRoles
                //        join sa in db.Roles on s.RoleId equals sa.Id
                //        where s.UserId == user.Id
                //        select sa.Name;

                //var roleName = query.FirstOrDefault();

                //var issuer = config["Jwt:Issuer"];
                //var audience = config["Jwt:Audience"];
                //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                //var claims = new List<Claim>
                //    {
                //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //        new Claim(ClaimTypes.Email, user.Email??user.UserName),
                //        new Claim(ClaimTypes.Name, user.Name),
                //        new Claim(ClaimTypes.Role, roleName),
                //    };

                //var token = new JwtSecurityToken(
                //    issuer: issuer,
                //    audience: audience,
                //    signingCredentials: credentials,
                //    claims: claims,
                //    expires: DateTime.UtcNow.AddHours(24));

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var accessToken = tokenHandler.WriteToken(token);
                //return Results.Ok(new
                //{
                //    accessToken,
                //    user.Email,
                //    user.Name,
                //    roleName,
                //    permissions,
                //    user.IsFirstTimeLogin
                //});
                var result =await _userService.Login(input);
                return Ok(new ApiResponse<object>
                {
                    Status = true,
                    Message = "LoginSuccess",
                    Data = null
                });
            }


            return Ok(user);
        }

        [HttpPost]
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
    }
}
