using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;

namespace WebCoreAPI.Repositories.Common
{
    public class Repository<T> : IRepository<T> where T : class, IKeyEntity<int>
    {
        private AppDbContext _dbContext;
        private DbSet<T> table;

        public Repository(AppDbContext dbContext)
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
        
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }


        public T GetById(object id)
        {
            return table.Find(id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return table.FirstOrDefault(predicate);
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
