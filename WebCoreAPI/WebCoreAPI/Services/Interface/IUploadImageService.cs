using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebCoreAPI.Services
{
    public interface IUploadImageService
    {
        public Task<BlobClient> UploadImageAsync(IFormFile input);
        public Task<(Uri uri, IEnumerable<BlobItem> blobItems)> GetImagesFromBlobAsync(int? segmentSize);
        Task<string> Upload(IFormFile file);
    }
}
