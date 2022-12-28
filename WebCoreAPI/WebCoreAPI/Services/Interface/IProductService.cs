using WebCoreAPI.Entity;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        ProductViewmodel GetOne(int id);
        Task CreateAsync(CreateProductModel model);
        Task UpdateAsync(int id, CreateProductModel product);
        Task DeleteAsync(int id);
    }
}
