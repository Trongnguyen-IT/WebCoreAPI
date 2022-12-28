using WebCoreAPI.Entity;
using WebCoreAPI.Models;

namespace WebCoreAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Category GetOne(int id);
        Task Create(CategoryCreateModel model);
        Task Update(int id, CategoryCreateModel model);
        Task Delete(int id);
    }
}
