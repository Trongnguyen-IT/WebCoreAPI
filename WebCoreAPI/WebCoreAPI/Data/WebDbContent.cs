using Microsoft.EntityFrameworkCore;

namespace WebCoreAPI.Data
{
    public class WebDbContent : DbContext
    {
        public WebDbContent(DbContextOptions options) : base(options)
        {
        }
    }
}
