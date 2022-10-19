using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Repositories.Common;

namespace WebCoreAPI.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
