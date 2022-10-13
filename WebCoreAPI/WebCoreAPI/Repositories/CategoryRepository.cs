using WebCoreAPI.Data;
using WebCoreAPI.Repositories.Common;

namespace WebCoreAPI.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebDbContext dbContext) : base(dbContext)
        {
        }
    }
}
