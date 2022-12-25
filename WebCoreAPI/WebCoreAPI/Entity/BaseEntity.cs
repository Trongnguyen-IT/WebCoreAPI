using System.ComponentModel.DataAnnotations;

namespace WebCoreAPI.Entity
{
    public class BaseEntity : IKeyEntity<int>
    {
        [Key]
        public virtual int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }
    }
}
