using WebCoreAPI.Data;
using WebCoreAPI.Repositories.Common;

namespace WebCoreAPI.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(WebDbContext dbContext) : base(dbContext)
        {
        }
    }
}
