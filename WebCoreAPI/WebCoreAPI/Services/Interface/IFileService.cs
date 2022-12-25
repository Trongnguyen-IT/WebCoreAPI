using WebCoreAPI.Entity;

namespace WebCoreAPI.Services
{
    public interface IFileService
    {
        Task<IEnumerable<FileEntity>> GetAll();
        Task Create(IFormFile file);
        Task Update();
        Task Delete();
    }
}
