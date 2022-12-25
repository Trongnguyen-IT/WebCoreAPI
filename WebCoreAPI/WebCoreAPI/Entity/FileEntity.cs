namespace WebCoreAPI.Entity
{
    public class FileEntity : BaseEntity
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public byte[]? Content { get; set; }
    }
}
