using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IProductService   _productService;
        public ProductController(ILogger<ProductController> logger, 
            IConfiguration configuration,
            IProductService productService)
        {
            _logger = logger;
            _configuration = configuration;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
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
