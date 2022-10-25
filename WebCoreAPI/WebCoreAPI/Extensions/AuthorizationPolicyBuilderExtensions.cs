using Microsoft.AspNetCore.Authorization;
using WebCoreAPI.Permission;

namespace WebCoreAPI.Extensions
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationOptions AddAuthorizationPolicy(this AuthorizationOptions option)
        {
            var source = typeof(UserFeatures);
            foreach (var item in source.GetFields())
            {
                var feature = (string)item.GetValue(source);
                option.AddPolicy(feature, policy => policy.Requirements.Add(new PermissionAuthorizationRequirement(feature)));
            }

            return option;
        }
    }
}
