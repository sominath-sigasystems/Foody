using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Cart
    {
        [Key] public int Id { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;
        public int? RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser? User { get; set; }
        public Restaurant? Restaurant { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }

        public decimal Total => CartItems?.Sum(ci => ci.SubTotal) ?? 0;
    }
}
