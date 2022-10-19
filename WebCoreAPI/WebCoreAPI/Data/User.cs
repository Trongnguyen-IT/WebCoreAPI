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

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole > UserRoles { get; set; }
    }
}
