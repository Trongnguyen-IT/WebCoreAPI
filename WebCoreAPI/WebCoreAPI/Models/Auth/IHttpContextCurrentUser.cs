namespace WebCoreAPI.Models.Auth
{
    public interface IHttpContextCurrentUser
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
