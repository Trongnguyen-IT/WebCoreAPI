using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCoreAPI.Models;
using WebCoreAPI.Services;

namespace WebCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImageUploadController : ControllerBase
    {
        private readonly StoreAccountAppSettings _storeAccountAppSettings;
        private readonly IUploadImageService _uploadImageService;
        public ImageUploadController(IOptions<StoreAccountAppSettings> options,
            IUploadImageService uploadImageService)
        {
            _storeAccountAppSettings = options.Value;
            _uploadImageService = uploadImageService;
        }


        [HttpGet("GetFromBlob")]
        public async Task<IActionResult> GetFromBlob()
        {
            var (uri, blobItems) = await _uploadImageService.GetImagesFromBlobAsync(10);
            return Ok(new { uri, blobItems }); ;
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var filePath = await _uploadImageService.Upload (file);
            return Ok(filePath);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageToBlobStore(IFormFile file)
        {
            var result = await _uploadImageService.UploadImageAsync(file);
            return Ok(result);
        }
    }
}
