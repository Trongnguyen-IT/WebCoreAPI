using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Data;
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
        private readonly IProductService _productService;
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
        public IActionResult GetOne(int id)
        {
            return Ok(_productService.GetOne(id));
        }

        [HttpPost]
        public IActionResult Create(CreateProductModel model)
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
            _productService.Create(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(CreateProductModel product)
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
