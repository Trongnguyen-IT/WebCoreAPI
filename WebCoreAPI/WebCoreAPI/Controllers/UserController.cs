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
        //private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public UserController(
            //IUserService userService,
            UserManager<AppUser> userManager)
        {
            //_userService = userService;
            _userManager = userManager;
        }

        //[HttpPost]
        //public IActionResult Login(LoginModel model)
        //{
        //    var user = _userService.Login(model);
        //    return Ok(user);
        //}
        
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
                Password = input.Password
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
