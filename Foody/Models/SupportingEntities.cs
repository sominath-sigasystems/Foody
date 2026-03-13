using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    // ✅ Restaurant Operating Hours
    public class RestaurantTiming
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        public DayOfWeek Day { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public bool IsClosed { get; set; } = false;

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant? Restaurant { get; set; }
    }

    // ✅ Order Status Tracking History
    public class OrderStatusHistory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public OrderStatus Status { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }

    

    // ✅ Promo Codes / Discounts
    public class PromoCode
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DiscountType Type { get; set; } // Percentage or Fixed
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinOrderValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MaxDiscount { get; set; }

        public int UsageLimit { get; set; } = 0; // 0 = unlimited
        public int UsedCount { get; set; } = 0;

        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum DiscountType
    {
        Percentage = 1,
        Fixed = 2
    }
}
