using Microsoft.AspNetCore.Identity;

namespace WebCoreAPI.Data
{
    public class ApplicationUser  : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole > UserRoles { get; set; }
    }
}
