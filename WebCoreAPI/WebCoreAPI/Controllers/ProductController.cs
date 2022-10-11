using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Models;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _configuration;
        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var products = new List<Product>
            {
                new Product
                {
                    Id= Guid.NewGuid(),
                    Name ="product1",
                    Price = 1000
                }
            };
            return Ok(connectionString);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(Guid id)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "product1",
                Price = 1000
            };
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            return Ok(product);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(id);
        }
    }
}
