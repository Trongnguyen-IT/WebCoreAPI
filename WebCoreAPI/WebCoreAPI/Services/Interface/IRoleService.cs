using Microsoft.AspNetCore.Identity;
using WebCoreAPI.Entity;
using WebCoreAPI.Models.Permission;

namespace WebCoreAPI.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<AppRole>> GetAll();
        Task<IdentityResult> Assign(int userId, int roleId);
        Task AddPermission(PermissionCreateOrUpdateDto input);
    }
}
