using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Data;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateModel model)
        {
            var category = new Category
            {
                Name = model.Name
            };
            _categoryService.Create(category);
            return Ok();
        }
    }
}
