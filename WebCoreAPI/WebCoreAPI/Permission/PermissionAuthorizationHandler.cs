using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebCoreAPI.DbContext;
using WebCoreAPI.Models.Auth;

namespace WebCoreAPI.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        //private readonly IServiceScopeFactory scopeFactory;
        public PermissionAuthorizationHandler(
            //IServiceScopeFactory scopeFactory
            )
        {
            //this.scopeFactory = scopeFactory;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            //using (var scope = scopeFactory.CreateScope())
            //{
                bool isAuthenticated = context.User.Identity.IsAuthenticated;
                if (!isAuthenticated)
                {
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);

                return Task.CompletedTask;
            //}

        }
    }
}
