using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Data;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(Category model)
        {
            _categoryRepository.Insert(model);
            _categoryRepository.Save();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll().ToListAsync();
        }

        public Category GetOne(int id)
        {
            throw new NotImplementedException();
        }
    }
}
