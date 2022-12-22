using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using WebCoreAPI.Models;

namespace WebCoreAPI.Services
{
    public interface IUploadImageService
    {
        public Task<BlobClient> UploadImageAsync(IFormFile input);
        public Task<(Uri uri, IEnumerable<BlobItem> blobItems)> GetImagesAsync(int? segmentSize);
        Task CreateImage(ImageCreateOrUpdateDto input);
    }
}
