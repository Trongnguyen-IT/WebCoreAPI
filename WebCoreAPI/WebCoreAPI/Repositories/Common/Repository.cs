using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Data;

namespace WebCoreAPI.Repositories.Common
{
    public class Repository<T> : IRepository<T> where T : class, IKeyEntity<int>
    {
        private WebDbContext _dbContext;
        private DbSet<T> table;

        public Repository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public IQueryable<T> GetAll()
        {
            return table.AsQueryable();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }
    }
}
