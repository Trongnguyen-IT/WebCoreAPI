using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebCoreAPI.DbContext
{
    public class GenericDbContext<T> : IGenericDbContext<T> where T : Microsoft.EntityFrameworkCore.DbContext, IDisposable
    {
        // Flag: Has Dispose already been called? recheck solution???
        private bool disposed = false;

        private readonly T _dbContext;

        public GenericDbContext(T dataContext)
        {
            _dbContext = dataContext;
        }

        public DatabaseFacade Database { get { return _dbContext.Database; } }

        public DbSet<TEntity> Repository<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }

        public T GetContext()
        {
            return _dbContext;
        }

        /// <summary>
        /// execute row sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        /// <summary>
        /// execute row sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteSqlRaw(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        /// <summary>
        /// Get detail add/update/delete informations
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, (int, int, int)> GetAddUpdateDeleteEntryCount()
        {
            var entities = _dbContext.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged);
            Dictionary<string, (int, int, int)> dic = new Dictionary<string, (int, int, int)>();
            entities.GroupBy(x => x.Entity.GetType().Name).ToList().ForEach(group =>
            {
                var key = group.First().Entity.GetType().Name;
                var value = (
                    group.Where(x => x.State == EntityState.Added).Count(),
                    group.Where(x => x.State == EntityState.Modified).Count(),
                    group.Where(x => x.State == EntityState.Deleted).Count()
                );
                dic.Add(key, value);
            });
            return dic;
        }

        /// <summary>
        /// Get instance of dbcontext in a serviceScope identified by type of 'T'.
        /// </summary>
        public T GetContextScoped(IServiceScope serviceScope)
        {
            if (serviceScope != null)
            {
                return serviceScope.ServiceProvider.GetRequiredService<T>();
            }

            return GetContext();
        }
    }
}