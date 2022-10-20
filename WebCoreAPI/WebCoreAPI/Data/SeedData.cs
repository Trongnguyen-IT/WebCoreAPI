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
            //using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            //{
                //var context = serviceProvider.GetService<AppDbContext>();
                //var _roleManager = serviceProvider.GetService<RoleManager<AppRole>>();
                //var _userManager = serviceProvider.GetService<UserManager<AppUser>>();

                //await SeedRoles(_roleManager);
                //await SeedUsers(_userManager, testUserPw);
            //}

            using (var _roleManager = serviceProvider.GetService<RoleManager<AppRole>>())
            {
                await SeedRoles(_roleManager);
            }

            using (var _userManager = serviceProvider.GetService<UserManager<AppUser>>())
            {
                await SeedUsers(_userManager, testUserPw);
            }
        }

        public async static Task SeedRoles(RoleManager<AppRole> roleManager)
        {
            string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            foreach (string role in roles)
            {
                var existRole =await roleManager.FindByNameAsync(role);
                if (existRole == null)
                {
                    await roleManager.CreateAsync(new AppRole
                    {
                        Name = role
                    });
                }
            }
        }

        public async static Task  SeedUsers(UserManager<AppUser> userManager, string testUserPw = "")
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

                IdentityResult result =await userManager.CreateAsync(user, testUserPw);

                if (result.Succeeded)
                {
                   await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
        }
    }
}
