﻿using WebCoreAPI.Models;

namespace WebCoreAPI.Services
{
    public interface IUserService
    {
        public ApiResponse<object> Login(LoginModel model);
    }
}
