using WebCoreAPI.Data;
using WebCoreAPI.Models;

namespace WebCoreAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Category GetOne(int id);
        void Create(CategoryCreateModel model);
        void Update(int id, CategoryCreateModel model);
        void Delete(int id);
    }
}
