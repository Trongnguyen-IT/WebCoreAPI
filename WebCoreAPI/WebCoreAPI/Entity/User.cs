using Microsoft.AspNetCore.Identity;
using WebCoreAPI.Enum;

namespace WebCoreAPI.Entity
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }
        public UserType UseType { get; set; } = UserType.Employee;
        public bool IsFirstTimeLogin { get; set; } = true;

        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
