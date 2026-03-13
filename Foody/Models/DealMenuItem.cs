using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class DealMenuItem
    {
        [Key] public int Id { get; set; }
        [Required] public int DealId { get; set; }
        [Required] public int MenuItemId { get; set; }
        public decimal? OverrideDiscountValue { get; set; }
        public bool IsFreeItem { get; set; }
        public int ComboSequence { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public Deal? Deal { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
