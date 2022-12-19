using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    public class ImageUploadController : ControllerBase
    {
        private readonly StoreAccountAppSettings _storeAccountAppSettings;
        private readonly IUploadImageService  _uploadImageService;
        public ImageUploadController(IOptions<StoreAccountAppSettings> options,
            IUploadImageService uploadImageService)
        {
            _storeAccountAppSettings = options.Value;
            _uploadImageService = uploadImageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(); ;
        }


        [HttpPost("upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _uploadImageService.UploadImageAsync(file);
            return Ok(result);
        }
    }
}
