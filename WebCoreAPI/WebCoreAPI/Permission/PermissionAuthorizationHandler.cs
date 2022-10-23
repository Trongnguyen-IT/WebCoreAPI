using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebCoreAPI.DbContext;
using WebCoreAPI.Models.Auth;

namespace WebCoreAPI.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IServiceScopeFactory scopeFactory;
        public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var userId = context.User.FindFirstValue(DefineClaimTypes.UserId);
                var isAuthenticated = context.User.Identity.IsAuthenticated;
                if (!isAuthenticated)
                {
                    return Task.CompletedTask;
                }
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var user = db.Users.FirstOrDefault(p => p.Id == Convert.ToInt32(userId));
                if (user != null && user.UserName == "admin")
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
                return Task.CompletedTask;
            }

        }
    }
}
