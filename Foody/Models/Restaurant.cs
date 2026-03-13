using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Restaurant
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(200)] public string Name { get; set; } = string.Empty;
        [StringLength(500)] public string? Description { get; set; }
        [Required, StringLength(500)] public string Address { get; set; } = string.Empty;
        [StringLength(100)] public string City { get; set; } = string.Empty;
        [StringLength(50)] public string State { get; set; } = string.Empty;
        [StringLength(20)] public string Pincode { get; set; } = string.Empty;
        [StringLength(20)] public string Phone { get; set; } = string.Empty;
        [StringLength(500)] public string? ImageUrl { get; set; }
        [StringLength(500)] public string? CoverImageUrl { get; set; }

        public decimal MinimumOrder { get; set; }
        public decimal DeliveryFee { get; set; }
        public int EstimatedDeliveryTime { get; set; }
        public bool IsOpen { get; set; } = true;
        public TimeSpan OpeningTime { get; set; } = TimeSpan.FromHours(9);
        public TimeSpan ClosingTime { get; set; } = TimeSpan.FromHours(23);
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        [Required] public string OwnerId { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser? Owner { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<RestaurantTiming>? Timings { get; set; }
        public ICollection<Deal>? Deals { get; set; }  // ← ADD THIS
    }
}
