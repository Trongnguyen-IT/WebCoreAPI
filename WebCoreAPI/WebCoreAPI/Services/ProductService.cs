using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Data;
using WebCoreAPI.GenericDbContext;
using WebCoreAPI.Models;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IGenericDbContext<WebDbContext> _genericDbContext;

        public ProductService(IProductRepository productRepository,
            IGenericDbContext<WebDbContext> genericDbContext,
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
                    ImageUrl = product.ImageUrl,
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

        public void Create(CreateProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Promotion = model.Promotion,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity,

            };
            _productRepository.Insert(product);
            _productRepository.Save();
        }

        public void Update(int id, CreateProductModel model)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Promotion = model.Promotion;
                product.ImageUrl = model.ImageUrl;
                product.CategoryId = model.CategoryId;
                product.Quantity = model.Quantity;
                product.LastModified = DateTime.Now;

                _productRepository.Update(product);
                _productRepository.Save();
            }
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
        }
    }
}
