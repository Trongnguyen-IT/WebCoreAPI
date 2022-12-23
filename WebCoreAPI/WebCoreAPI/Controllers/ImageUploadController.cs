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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _uploadImageService.GetAllAsync()); ;
        }

        [HttpGet("GetFromBlob")]
        public async Task<IActionResult> GetFromBlob()
        {
            var (uri, blobItems) = await _uploadImageService.GetImagesFromBlobAsync(10);
            return Ok(new { uri, blobItems }); ;
        }


        [HttpPost]
        public async Task<IActionResult> CreateImage(ImageCreateOrUpdateDto input)
        {
            await _uploadImageService.CreateImage(input);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _uploadImageService.UploadImageAsync(file);
            return Ok(result);
        }
    }
}
