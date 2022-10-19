using Microsoft.AspNetCore.Identity;

namespace WebCoreAPI.Data
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationRole  Role { get; set; }

        public virtual ApplicationUser  User { get; set; }
    }
}
