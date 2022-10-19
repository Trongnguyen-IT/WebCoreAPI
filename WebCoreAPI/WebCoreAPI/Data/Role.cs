
using Microsoft.AspNetCore.Identity;

namespace WebCoreAPI.Data
{
    public class AppRole : IdentityRole<int>
    {
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
