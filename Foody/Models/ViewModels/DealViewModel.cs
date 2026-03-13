namespace Foody.Models.ViewModels
{
    public class DealViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? BadgeText { get; set; }

        public DealType Type { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public decimal? MinOrderValue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsFlashDeal { get; set; }
        public TimeSpan? RemainingTime { get; set; }

        public int? TotalUsageLimit { get; set; }
        public int UsedCount { get; set; }
        public int? RemainingUses => TotalUsageLimit.HasValue ? TotalUsageLimit.Value - UsedCount : null;

        public bool IsActive { get; set; }
        public string RestaurantName { get; set; } = string.Empty;

        // ✅ Computed properties for UI
        public string DiscountDisplay => Type switch
        {
            DealType.Percentage => $"{DiscountValue}% OFF",
            DealType.Fixed => $"₹{DiscountValue} OFF",
            DealType.BOGO => "BUY 1 GET 1 FREE",
            DealType.Combo => $"COMBO @ ₹{DiscountValue}",
            DealType.FreeDelivery => "FREE DELIVERY",
            DealType.FreeItem => "FREE ITEM",
            _ => "SPECIAL OFFER"
        };

        public string TimerDisplay => RemainingTime?.ToString(@"hh\:mm\:ss") ?? string.Empty;
        public int ProgressPercentage => TotalUsageLimit.HasValue
            ? (int)((decimal)UsedCount / TotalUsageLimit.Value * 100)
            : 0;

    }
}
