using Microsoft.AspNetCore.Mvc;
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
            _categoryService.Create(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryCreateModel model)
        {
            _categoryService.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}
