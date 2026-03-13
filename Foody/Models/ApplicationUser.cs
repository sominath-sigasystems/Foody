using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Foody.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ProfileImageUrl { get; set; }

        // Navigation properties (required by DbContext)
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<Order>? PlacedOrders { get; set; }
        public ICollection<Order>? AssignedDeliveries { get; set; }
        public ICollection<Restaurant>? OwnedRestaurants { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<PaymentMethod>? PaymentMethods { get; set; }
    }
}
