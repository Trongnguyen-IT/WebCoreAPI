using System.Linq.Expressions;

namespace WebCoreAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        void Insert(T obj);
        Task InsertAsync(T obj);
        void Update(T obj);
        void Delete(object id);
        Task UpdateAsync(T obj);
        Task DeleteAsync(object id);
    }
}
