using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using WebCoreAPI.Models;

namespace WebCoreAPI.Services
{
    public class UploadImageService : IUploadImageService
    {
        private readonly StoreAccountAppSettings _storeAccountAppSettings;

        public UploadImageService(IOptions<StoreAccountAppSettings> options)
        {
            _storeAccountAppSettings = options.Value;
        }

        public async Task<BlobClient> UploadImageAsync(IFormFile file)
        {
            //string localPath = "uploads";
            //Directory.CreateDirectory(localPath);
            //string fileName = $"{file.FileName}";
            //string localFilePath = Path.Combine(localPath, fileName);
            var blobServiceClient = new BlobServiceClient(_storeAccountAppSettings.AzureWebJobsStorage);
            var container = await GetBlobContainerClient(blobServiceClient, _storeAccountAppSettings.ContainerName);
            BlobClient blobClient = container.GetBlobClient(file.FileName);
            using (var fileStream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(fileStream, true);
            }
            return blobClient;
        }

        public async Task<BlobClient> UploadStream(BlobContainerClient containerClient, string localFilePath)
        {
            string fileName = Path.GetFileName(localFilePath);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (var fileStream = File.OpenRead(localFilePath))
            {
                await blobClient.UploadAsync(fileStream, true);
            }
            return blobClient;
        }

        public async Task<BlobContainerClient> GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            var container = blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            return container;
        }
    }
}
