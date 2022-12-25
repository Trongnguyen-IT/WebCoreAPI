using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
