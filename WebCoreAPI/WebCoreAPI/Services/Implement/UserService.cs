using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebCoreAPI.Entity;
using WebCoreAPI.Migrations;
using WebCoreAPI.Models;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class UserService : IUserService
    {
        //private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly UserStore<AppUser>  _userStore;

        public UserService(
            //IUserRepository userRepository,
            UserStore<AppUser>  userStore
            IOptionsMonitor<AppSettings> optionsMonitor)
        {
            //_userRepository = userRepository;
            _userStore = _userStore;
            _appSettings = optionsMonitor.CurrentValue;
        }

        public ApiResponse<object> Login(UserLoginDto model)
        {
            var user = new AppUser();// _userRepository.GetSingle(p => p.UserName == model.UserName && p.Password == model.Password);
            if (user == null)
            {
                return new ApiResponse<object>()
                {
                    Status = false,
                    Message = "LoginFail",
                    Data = null
                };
            }

            var token = GenerateToken(user);

            return new ApiResponse<object>()
            {
                Status = true,
                Message = "LoginSucess",
                Data = token
            };
        }

        private string GenerateToken(AppUser  user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
