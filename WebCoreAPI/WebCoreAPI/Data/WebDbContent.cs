using Microsoft.EntityFrameworkCore;

namespace WebCoreAPI.Data
{
    public class WebDbContent : DbContext
    {
        public WebDbContent(DbContextOptions<WebDbContent> options) : base(options)
        {
        }

        #region DbSet

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        #endregion

    }
}
