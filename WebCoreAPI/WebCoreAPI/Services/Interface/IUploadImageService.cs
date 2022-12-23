using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using WebCoreAPI.Entity;
using WebCoreAPI.Models;

namespace WebCoreAPI.Services
{
    public interface IUploadImageService
    {
        public Task<BlobClient> UploadImageAsync(IFormFile input);
        public Task<(Uri uri, IEnumerable<BlobItem> blobItems)> GetImagesFromBlobAsync(int? segmentSize);
        public Task<IEnumerable<Image>> GetAllAsync();
        Task CreateImage(ImageCreateOrUpdateDto input);
    }
}
