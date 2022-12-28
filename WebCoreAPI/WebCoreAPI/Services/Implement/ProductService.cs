using Microsoft.EntityFrameworkCore;
using WebCoreAPI.DbContext;
using WebCoreAPI.Entity;
using WebCoreAPI.Models;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGenericDbContext<AppDbContext> _genericDbContext;

        public ProductService(IProductRepository productRepository,
            IGenericDbContext<AppDbContext> genericDbContext,
            ICategoryRepository categoryRepository)
        {
            _genericDbContext = genericDbContext;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll().ToListAsync();
        }

        public ProductViewmodel GetOne(int id)
        {
            var productView = new ProductViewmodel();
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                productView = new ProductViewmodel
                {
                    Name = product.Name,
                    Price = product.Price,
                    Promotion = product.Promotion,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId,
                    CreatedBy = product.CreatedBy,
                    DateCreated = product.DateCreated,
                    LastModified = product.LastModified,
                };

                var category = product.CategoryId.HasValue
                        ? _categoryRepository.GetById(product.CategoryId.Value)
                        : null;
                if (category != null)
                {
                    productView.Category = new CategoryViewModel(category.Name);
                }
            }

            return productView;
        }

        public async Task CreateAsync(CreateProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Promotion = model.Promotion,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity,
                AvatarUrl = model.AvatarUrl,
                ImageUrls = model.ImageUrls
            };
            await _productRepository.InsertAsync(product);
        }

        public async Task UpdateAsync(int id, CreateProductModel model)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Promotion = model.Promotion;
                product.CategoryId = model.CategoryId;
                product.Quantity = model.Quantity;
                product.LastModified = DateTime.Now;
                product.AvatarUrl = model.AvatarUrl;
                product.ImageUrls = model.ImageUrls;

                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
