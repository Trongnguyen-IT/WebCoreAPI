using WebCoreAPI.Entity;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        ProductViewmodel GetOne(int id);
        void Create(CreateProductModel model);
        void Update(int id, CreateProductModel product);
        void Delete(int id);
    }
}
