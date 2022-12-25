using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;

namespace WebCoreAPI.Repositories
{
    public class FileRepository : Repository<FileEntity>, IFileRepository
    {
        public FileRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
