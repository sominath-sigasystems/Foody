using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Deal
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(200)] public string Title { get; set; } = string.Empty;
        [StringLength(500)] public string? Description { get; set; }
        [StringLength(500)] public string? ImageUrl { get; set; }
        [StringLength(100)] public string? BadgeText { get; set; }

        public DealType Type { get; set; } = DealType.Percentage;
        public decimal DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public decimal? MinOrderValue { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public bool IsFlashDeal { get; set; }
        public TimeSpan? DailyStartTime { get; set; }
        public TimeSpan? DailyEndTime { get; set; }

        public int? TotalUsageLimit { get; set; }
        public int? PerUserUsageLimit { get; set; }
        public int UsedCount { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int? RestaurantId { get; set; }

        // Navigation properties
        public Restaurant? Restaurant { get; set; }
        public ICollection<DealCategory>? DealCategories { get; set; }
        public ICollection<DealMenuItem>? DealMenuItems { get; set; }
        public ICollection<DealUsage>? DealUsages { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }

    // ✅ Enum: Deal Types
    public enum DealType
    {
        /// <summary>Percentage off (e.g., 20% OFF)</summary>
        Percentage = 1,

        /// <summary>Fixed amount off (e.g., ₹100 OFF)</summary>
        Fixed = 2,

        /// <summary>Buy One Get One Free</summary>
        BOGO = 3,

        /// <summary>Fixed price for combo (e.g., Meal Deal @ ₹499)</summary>
        Combo = 4,

        /// <summary>Free delivery</summary>
        FreeDelivery = 5,

        /// <summary>Free item with minimum purchase</summary>
        FreeItem = 6
    }
}
