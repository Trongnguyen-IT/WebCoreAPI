namespace WebCoreAPI.Models
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
