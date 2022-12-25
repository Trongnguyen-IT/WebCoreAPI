using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        public async Task<IActionResult> Get()
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
            _productService.Create(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateProductModel model)
        {
            _productService.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok();
        }
    }
}
