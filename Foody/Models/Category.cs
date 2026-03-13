using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
        [StringLength(500)] public string? Description { get; set; }
        [StringLength(500)] public string? ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [Required] public int RestaurantId { get; set; }

        // Navigation properties
        public Restaurant? Restaurant { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
        public ICollection<DealCategory>? DealCategories { get; set; }  // ← ADD THIS
    }
}
