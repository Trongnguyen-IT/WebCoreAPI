using Microsoft.EntityFrameworkCore;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;

namespace WebCoreAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw = "")
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                SeedDB(context, testUserPw);
            }
        }

        public static void SeedDB(AppDbContext context, string adminID)
        {
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.AddRange(
                new AppUser
                {
                    FullName= "Admin",
                    IsActive= true,
                    Email="admin@gmail.com",
                    UserName="admin",
                });
            context.SaveChanges();
        }
    }
}
