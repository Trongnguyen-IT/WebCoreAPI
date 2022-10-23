using System.Security.Claims;

namespace WebCoreAPI.Models.Auth
{
    public class HttpContextCurrentUser : IHttpContextCurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var _user = _httpContextAccessor.HttpContext?.User;
            UserId = _user.FindFirstValue(DefineClaimTypes.UserId);
            Email = _user.FindFirstValue(ClaimTypes.Email);
        }

        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
