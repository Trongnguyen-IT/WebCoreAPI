using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreAPI.Entity
{
   
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int? Promotion { get; set; }
        public int Quantity { get; set; }
        public string? AvatarUrl { get; set; }
        public string[]? ImageUrls { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
    }
}
