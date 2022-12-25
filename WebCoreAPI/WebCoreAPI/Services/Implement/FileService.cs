using Azure.Storage.Blobs;
using WebCoreAPI.Entity;
using WebCoreAPI.Models;
using WebCoreAPI.Repositories;

namespace WebCoreAPI.Services
{
    public class FileService : IFileService
    {
        public readonly IFileRepository _fileRepository;
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task Create(IFormFile file)
        {
            string localPath = "uploads";
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            var localFilePath = Path.Combine(localPath, $"{file.Name}_{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}");

            await using (FileStream fileStream = new(localFilePath, FileMode.Create))
            {
                //file.CopyTo(fileStream);
                var stream = file.OpenReadStream();
                BinaryReader reader = new BinaryReader(stream);

                byte[] buffer = new byte[fileStream.Length];

                reader.Read(buffer, 0, buffer.Length);

                await _fileRepository.InsertAsync(new FileEntity
                {
                    Name = file.FileName,
                    Content= buffer
                });

                fileStream.Close();
            }
        }

        public async Task Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
