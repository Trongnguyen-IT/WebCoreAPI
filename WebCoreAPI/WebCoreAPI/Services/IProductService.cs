using WebCoreAPI.Data;
using WebCoreAPI.Services;

namespace WebCoreAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Product GetOne(int id);
        void Create(Product model);
    }
}
