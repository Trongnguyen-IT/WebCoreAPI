using WebCoreAPI.Data;
using WebCoreAPI.Repositories.Common;

namespace WebCoreAPI.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }
    }
}
