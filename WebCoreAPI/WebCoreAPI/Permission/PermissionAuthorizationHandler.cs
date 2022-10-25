﻿using Microsoft.AspNetCore.Authorization;
using WebCoreAPI.DbContext;
using WebCoreAPI.Models.Auth;
using WebCoreAPI.Models.Permission;

namespace WebCoreAPI.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IServiceScopeFactory scopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var userIdClaim = context.User.FindFirst(DefineClaimTypes.UserId);

                if (userIdClaim is null)
                {
                    return Task.CompletedTask;
                }
                var userId = Convert.ToInt32(userIdClaim.Value);
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var query = from s in db.UserRoles
                            join sa in db.RoleClaims on s.RoleId equals sa.RoleId
                            where s.UserId == userId && sa.ClaimType == Permissions.Type && sa.ClaimValue == requirement.Permission
                            select 1;
                if (query.Any())
                    context.Succeed(requirement);
                else
                    context.Fail();

                return Task.CompletedTask;
            }
        }
    }
}
