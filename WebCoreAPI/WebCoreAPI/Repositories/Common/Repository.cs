using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;

namespace WebCoreAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IKeyEntity<int>
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> table;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
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
            _dbContext.Set<T>().Add(obj);
            _dbContext.SaveChanges();
        }

        public async Task InsertAsync(T obj)
        {
            table.Add(obj);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            await _dbContext.SaveChangesAsync();
        }
    }
}
