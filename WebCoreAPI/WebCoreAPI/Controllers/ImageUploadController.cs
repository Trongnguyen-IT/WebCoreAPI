using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Get()
        {
            var (uri,blobItems) = await _uploadImageService.GetImagesAsync(10);
            return Ok(new { uri,blobItems }); ;
        }


        [HttpPost("upload/image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _uploadImageService.UploadImageAsync(file);
            return Ok(result);
        }
    }
}
