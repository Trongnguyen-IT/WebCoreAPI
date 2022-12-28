using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Entity;
using WebCoreAPI.Models;
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

        public async Task Create(CategoryCreateModel model)
        {
            var category = new Category
            {
                Name = model.Name
            };

            await _categoryRepository.InsertAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll().ToListAsync();
        }

        public Category GetOne(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public async Task Update(int id, CategoryCreateModel model)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                category.Name = model.Name;
                category.LastModified = DateTime.Now;

                await _categoryRepository.UpdateAsync(category);
            }
        }

        public async Task Delete(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
