using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class DealCategory
    {
        [Key] public int Id { get; set; }
        [Required] public int DealId { get; set; }
        [Required] public int CategoryId { get; set; }
        public decimal? OverrideDiscountValue { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        public Deal? Deal { get; set; }
        public Category? Category { get; set; }
    }
}
