using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebCoreAPI.Data
{
    public class WebDbContext : IdentityDbContext<ApplicationUser , ApplicationRole , int, IdentityUserClaim<int>, ApplicationUserRole , IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
        }

        #region DbSet

        public override DbSet<ApplicationUser > Users { get; set; }
        public override DbSet<ApplicationRole > Roles { get; set; }
        public override  DbSet<ApplicationUserRole >  UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    // Each User can have many UserClaims
            //    b.HasMany(e => e.Claims)
            //        .WithOne()
            //        .HasForeignKey(uc => uc.UserId)
            //        .IsRequired();

            //    // Each User can have many UserLogins
            //    b.HasMany(e => e.Logins)
            //        .WithOne()
            //        .HasForeignKey(ul => ul.UserId)
            //        .IsRequired();

            //    // Each User can have many UserTokens
            //    b.HasMany(e => e.Tokens)
            //        .WithOne()
            //        .HasForeignKey(ut => ut.UserId)
            //        .IsRequired();

            //    // Each User can have many entries in the UserRole join table
            //    b.HasMany(e => e.UserRoles)
            //        .WithOne(e => e.User)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<ApplicationRole>(b =>
            //{
            //    // Each Role can have many entries in the UserRole join table
            //    b.HasMany(e => e.UserRoles)
            //        .WithOne(e => e.Role)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();
            //});
        }
    }
}
