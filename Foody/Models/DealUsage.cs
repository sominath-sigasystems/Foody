using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class DealUsage
    {
        [Key] public int Id { get; set; }
        [Required] public int DealId { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;
        [Required] public int OrderId { get; set; }
        public decimal DiscountApplied { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;

        public Deal? Deal { get; set; }
        public ApplicationUser? User { get; set; }
        public Order? Order { get; set; }
    }
}
