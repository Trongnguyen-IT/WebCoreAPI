using System.ComponentModel.DataAnnotations;

namespace WebCoreAPI.Models
{
    public class CreateProductModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Promotion { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public string? AvatarUrl { get; set; }
        public string[]? ImageUrls { get; set; }
    }
}
