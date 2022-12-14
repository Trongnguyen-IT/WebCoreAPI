using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebCoreAPI.Entity;
using WebCoreAPI.Models;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class UploadImageService : IUploadImageService
    {
        private readonly StoreAccountAppSettings _storeAccountAppSettings;
        public static IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadImageService(IOptions<StoreAccountAppSettings> options,
            IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            _storeAccountAppSettings = options.Value;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<IEnumerable<BlobItem>> ListBlobsHierarchicalListing(BlobContainerClient container,
            string prefix,
            int? segmentSize)
        {
            try
            {
                var blobItems = new List<BlobItem>();
                // Call the listing operation and return pages of the specified size.
                var resultSegment = container.GetBlobsByHierarchyAsync(prefix: prefix, delimiter: "/")
                    .AsPages(default, segmentSize);

                // Enumerate the blobs returned for each page.
                await foreach (Azure.Page<BlobHierarchyItem> blobPage in resultSegment)
                {
                    // A hierarchical listing may return both virtual directories and blobs.
                    foreach (BlobHierarchyItem blobhierarchyItem in blobPage.Values)
                    {
                        if (blobhierarchyItem.IsPrefix)
                        {
                            // Write out the prefix of the virtual directory.
                            Console.WriteLine("Virtual directory prefix: {0}", blobhierarchyItem.Prefix);

                            // Call recursively with the prefix to traverse the virtual directory.
                            var res = await ListBlobsHierarchicalListing(container, blobhierarchyItem.Prefix, null);
                            blobItems.AddRange(res);
                        }
                        else
                        {
                            // Write out the name of the blob.
                            Console.WriteLine("Blob name: {0}", blobhierarchyItem.Blob.Name);
                            blobItems.Add(blobhierarchyItem.Blob);
                        }
                    }

                    Console.WriteLine();
                }

                return blobItems;
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task<(Uri uri, IEnumerable<BlobItem> blobItems)> GetImagesFromBlobAsync(int? segmentSize)
        {
            var blobServiceClient = new BlobServiceClient(_storeAccountAppSettings.AzureWebJobsStorage);
            var container = await GetBlobContainerClient(blobServiceClient, _storeAccountAppSettings.ContainerName);
            var uri = container.Uri;
            var blobItems = await ListBlobsHierarchicalListing(container, "uploads", 10);
            return (uri, blobItems);// blobNames.Select(p =>$"{containerUri }/{ p }");
        }

        public async Task<BlobClient> UploadImageAsync(IFormFile file)
        {
            string localPath = "uploads";
            Directory.CreateDirectory(localPath);
            var localFilePath = Path.Combine(localPath, $"{file.Name}_{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");

            var blobServiceClient = new BlobServiceClient(_storeAccountAppSettings.AzureWebJobsStorage);
            var container = await GetBlobContainerClient(blobServiceClient, _storeAccountAppSettings.ContainerName);
            BlobClient blobClient = container.GetBlobClient(localFilePath);

            await using (FileStream fileStream = new(localFilePath, FileMode.Create))
            {
                //file.CopyTo(fileStream);
                var stream = file.OpenReadStream();
                BinaryReader reader = new BinaryReader(stream);

                byte[] buffer = new byte[fileStream.Length];

                reader.Read(buffer, 0, buffer.Length);

                BinaryData binaryData = new BinaryData(buffer);

                await blobClient.UploadAsync(stream, true);

                fileStream.Close();
            }

            return blobClient;
        }

        public async Task<BlobContainerClient> GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            var container = blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

            return container;
        }

        public async Task<string> Upload(IFormFile file)
        {
            var vituralPath = "\\images\\";
            string serverPath = $"{_environment.WebRootPath}{vituralPath}";
            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            var fileName = $"{file.Name}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var serverFilePath = Path.Combine(serverPath, fileName);
            await using (var fileStream = File.Create(serverFilePath))
            {
                file.CopyTo(fileStream);
                fileStream.Close();
            }

            var scheme = _httpContextAccessor.HttpContext?.Request?.Scheme;
            var baseUrl = _httpContextAccessor.HttpContext?.Request?.Host.Value;

            return $"{vituralPath}{fileName}";
        }
    }
}
