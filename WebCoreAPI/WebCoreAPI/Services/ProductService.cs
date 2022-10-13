using Microsoft.EntityFrameworkCore;
using WebCoreAPI.Data;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository  _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll().ToListAsync();
        }
    }
}
