using WebCoreAPI.Models;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApiResponse<object> Login(LoginModel model)
        {
            var user = _userRepository.FindBy(p => p.UserName == model.UserName);
            var result = new ApiResponse<object>()
            {
                Status = true,
                Message = "LoginSucess",
                Data = user
            };
            return result;
        }
    }
}
