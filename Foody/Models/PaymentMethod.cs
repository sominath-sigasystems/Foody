using Foody.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public PaymentMethodType Type { get; set; }

        // ✅ Encrypted/tokenized data (never store raw card details)
        [StringLength(500)]
        public string? Token { get; set; }

        [StringLength(100)]
        public string? Last4Digits { get; set; }
        [StringLength(50)]
        public string? CardBrand { get; set; }
        [StringLength(50)]
        public string? ExpiryMonth { get; set; }
        [StringLength(10)]
        public string? ExpiryYear { get; set; }

        public bool IsDefault { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Navigation: User who saved this payment method
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
