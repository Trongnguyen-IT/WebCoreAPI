using Azure.Storage.Blobs;

namespace WebCoreAPI.Services
{
    public interface IUploadImageService
    {
        public Task<BlobClient> UploadImageAsync(IFormFile file);
        public Task<IEnumerable<string>> GetImagesAsync(int? segmentSize);
    }
}
