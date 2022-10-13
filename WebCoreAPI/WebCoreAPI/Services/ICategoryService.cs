using WebCoreAPI.Data;

namespace WebCoreAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Category GetOne(int id);
        void Create(Category model);
    }
}
