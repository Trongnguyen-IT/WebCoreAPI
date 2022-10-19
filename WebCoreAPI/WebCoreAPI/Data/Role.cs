
using Microsoft.AspNetCore.Identity;

namespace WebCoreAPI.Data
{
    public class ApplicationRole : IdentityRole<int>
    {
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
