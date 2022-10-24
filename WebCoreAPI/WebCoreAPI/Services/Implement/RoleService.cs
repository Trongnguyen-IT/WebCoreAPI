using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security;
using WebCoreAPI.Entity;
using WebCoreAPI.Models.Auth;
using WebCoreAPI.Models.Permission;

namespace WebCoreAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IHttpContextCurrentUser _httpContextCurrentUser;
        public RoleService(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IHttpContextCurrentUser httpContextCurrentUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextCurrentUser = httpContextCurrentUser;
        }

        public async Task AddPermission(PermissionCreateOrUpdateDto input)
        {
           var role = await _roleManager.FindByIdAsync(input.RoleId.ToString());
            var claimsInRole =await _roleManager.GetClaimsAsync(role);
            foreach (var item in input.Permissions)
            {
                if (!claimsInRole.Any(a => a.Type.Equals(Permissions.Type, StringComparison.OrdinalIgnoreCase) && a.Value.Equals(item, StringComparison.OrdinalIgnoreCase)))
                {
                    await _roleManager.AddClaimAsync(role, new Claim(Permissions.Type, item));
                }
            }
        }

        public async Task<IdentityResult> Assign(int userId, int roleId)
        {
            var curentUser = new { _httpContextCurrentUser.UserId, _httpContextCurrentUser.Email };
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
