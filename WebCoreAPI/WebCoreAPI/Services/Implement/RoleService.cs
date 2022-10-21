using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Entity;

namespace WebCoreAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Assign(int userId, int roleId)
        {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                
                return await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task<IEnumerable<AppRole>> GetAll()
        {
            return await _roleManager.Roles.ToListAsync();
        }
    }
}
