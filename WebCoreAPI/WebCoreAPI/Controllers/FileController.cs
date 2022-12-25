using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Create(IFormFile file)
        {
            await _fileService.Create(file);
            return Ok();
        }
    }
}
