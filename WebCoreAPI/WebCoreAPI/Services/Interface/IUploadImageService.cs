using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebCoreAPI.Services
{
    public interface IUploadImageService
    {
        public Task<BlobClient> UploadImageAsync(IFormFile file);
        public Task<(Uri uri, IEnumerable<BlobItem> blobItems)> GetImagesAsync(int? segmentSize);
    }
}
