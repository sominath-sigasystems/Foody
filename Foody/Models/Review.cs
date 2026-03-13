using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Review
    {
        [Key] public int Id { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;
        public int? RestaurantId { get; set; }
        public int? MenuItemId { get; set; }
        [Range(1, 5)] public int Rating { get; set; }
        [StringLength(1000)] public string? Comment { get; set; }
        public string? Images { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int HelpfulCount { get; set; }

        public ApplicationUser? User { get; set; }
        public Restaurant? Restaurant { get; set; }
        public MenuItem? MenuItem { get; set; }
        public ICollection<ReviewReply>? Replies { get; set; }
    }
}
