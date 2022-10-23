using Microsoft.AspNetCore.Authorization;

namespace WebCoreAPI.Permission
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
