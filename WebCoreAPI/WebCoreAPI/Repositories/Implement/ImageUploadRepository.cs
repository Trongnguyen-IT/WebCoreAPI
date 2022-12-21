using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Repositories.Common;

namespace WebCoreAPI.Repositories
{
    public class ImageUploadRepository : Repository<Image>, IImageUploadRepository
    {
        public ImageUploadRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
