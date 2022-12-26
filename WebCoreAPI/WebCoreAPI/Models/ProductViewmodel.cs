namespace WebCoreAPI.Models
{
    public class ProductViewmodel
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? AvatarUrl { get; set; }
        public int? Promotion { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
