using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class MenuItem
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(200)] public string Name { get; set; } = string.Empty;
        [StringLength(1000)] public string? Description { get; set; }
        [StringLength(500)] public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public bool IsVeg { get; set; } = true;
        public bool IsAvailable { get; set; } = true;
        public bool IsPopular { get; set; }
        public bool IsFeatured { get; set; }
        public int PreparationTime { get; set; }
        public int Calories { get; set; }
        public string? Ingredients { get; set; }
        public string? Allergens { get; set; }
        public string? CustomizationOptions { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        [Required] public int CategoryId { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<MenuItemTag>? MenuItemTags { get; set; }  // ← ADD THIS
        public ICollection<DealMenuItem>? DealMenuItems { get; set; }  // ← ADD THIS
    }
}
