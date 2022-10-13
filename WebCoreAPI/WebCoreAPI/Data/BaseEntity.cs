namespace WebCoreAPI.Data
{
    public class BaseEntity : IKeyEntity<int>
    {
        public virtual int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
    }
}
