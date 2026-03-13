using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class OrderItem
    {

        [Key] public int Id { get; set; }
        [Required] public int OrderId { get; set; }
        [Required] public int MenuItemId { get; set; }
        [Required] public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public string? Customization { get; set; }
        public string? SpecialInstructions { get; set; }

        public Order? Order { get; set; }
        public MenuItem? MenuItem { get; set; }

        public decimal SubTotal => Quantity * UnitPrice;
    }
}
