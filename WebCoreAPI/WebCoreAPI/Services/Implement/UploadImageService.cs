using Azure;
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
        private async Task<IEnumerable<string>> ListBlobsHierarchicalListing(BlobContainerClient container,
            string prefix,
            int? segmentSize)
        {
            try
            {
                var blobNames = new List<string>();
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
                            blobNames.AddRange(res);
                        }
                        else
                        {
                            // Write out the name of the blob.
                            Console.WriteLine("Blob name: {0}", blobhierarchyItem.Blob.Name);
                            blobNames.Add(blobhierarchyItem.Blob.Name);
                        }
                    }

                    Console.WriteLine();
                }

                return blobNames;
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetImagesAsync(int? segmentSize)
        {
            var blobServiceClient = new BlobServiceClient(_storeAccountAppSettings.AzureWebJobsStorage);
            var container = await GetBlobContainerClient(blobServiceClient, _storeAccountAppSettings.ContainerName);
            var containerUri = container.Uri;
            var blobNames = await ListBlobsHierarchicalListing(container, "uploads", 10);
            return blobNames.Select(p =>$"{containerUri }/{ p }");
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
    }
}
