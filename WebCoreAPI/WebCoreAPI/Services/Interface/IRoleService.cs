using Microsoft.AspNetCore.Identity;
using WebCoreAPI.Entity;

namespace WebCoreAPI.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<AppRole>> GetAll();
        Task<IdentityResult> Assign(int userId, int roleId);
    }
}
