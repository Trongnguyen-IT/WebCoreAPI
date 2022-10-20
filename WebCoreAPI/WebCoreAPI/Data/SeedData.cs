using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Enum;

namespace WebCoreAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw = "")
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                //var context = serviceProvider.GetService<AppDbContext>();
                var _roleManager = serviceProvider.GetService<RoleManager<AppRole>>();
                var _userManager = serviceProvider.GetService<UserManager<AppUser>>();

                SeedRoles(_roleManager);
                SeedUsers(_userManager, testUserPw);
            }
        }

        public static void SeedRoles(RoleManager<AppRole> roleManager)
        {
            string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            foreach (string role in roles)
            {
                //var roleStore = new RoleStore<IdentityRole>(context);
                var existRole = roleManager.FindByNameAsync(role);
                if (existRole == null)
                {
                    roleManager.CreateAsync(new AppRole
                    {
                        Name = role
                    }).Wait();
                }
            }
        }

        public static void SeedUsers(UserManager<AppUser> userManager, string testUserPw = "")
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    FullName = "admin",
                    IsActive = true,
                    UseType = UserType.SuperAdmin
                };

                IdentityResult result = userManager.CreateAsync(user, testUserPw).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
