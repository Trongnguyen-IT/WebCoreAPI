using Microsoft.AspNetCore.Authorization;

namespace WebCoreAPI.Extensions
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationOptions AddAuthorizationPolicy(this AuthorizationOptions options)
        {
            var source = typeof(UserFeatures);
            foreach (var item in source.GetFields())
            {
                var feature = (string)item.GetValue(source);
                options.AddPolicy(feature, policyBuilder => policyBuilder.RequireClaim(feature));
            }

            return options;
        }
    }
}
