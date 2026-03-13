using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class CartItem
    {
        [Key] public int Id { get; set; }
        [Required] public int CartId { get; set; }
        [Required] public int MenuItemId { get; set; }
        [Required] public int Quantity { get; set; } = 1;
        public string? Customization { get; set; }
        public string? SpecialInstructions { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public Cart? Cart { get; set; }
        public MenuItem? MenuItem { get; set; }

        public decimal SubTotal => Quantity * (MenuItem?.DiscountedPrice ?? MenuItem?.Price ?? 0);
    }
}
