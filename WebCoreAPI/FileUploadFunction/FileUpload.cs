using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
namespace FileUploadFunction
{
    public  class FileUpload
    {
        private readonly StoreAccountAppSettings  _storeAccountAppSettings;
        public FileUpload(IOptions<StoreAccountAppSettings> options)
        {
            _storeAccountAppSettings = options.Value;
        }

        [FunctionName("FileUpload")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {
            Stream myBlob = new MemoryStream();
            var file = req.Form.Files[0];

            myBlob = file.OpenReadStream();
            var blobClient = new BlobContainerClient(_storeAccountAppSettings.AzureWebJobsStorage, _storeAccountAppSettings.ContainerName);
            var blob = blobClient.GetBlobClient(file.FileName);
            await blob.UploadAsync(myBlob);
            return new OkObjectResult("file uploaded successfylly");
        }
    }
}