using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Data;
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
        public void Create(CategoryCreateModel model)
        {
            var category = new Category
            {
                Name = model.Name
            };

            _categoryRepository.Insert(category);
            _categoryRepository.Save();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll().ToListAsync();
        }

        public Category GetOne(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public void Update(int id, CategoryCreateModel model)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                category.Name = model.Name;
                category.LastModified = DateTime.Now;

                _categoryRepository.Update(category);
                _categoryRepository.Save();
            }
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.Save();
        }
    }
}
