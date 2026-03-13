using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Address
    {
        [Key] public int Id { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Label { get; set; } = string.Empty;
        [Required, StringLength(500)] public string Street { get; set; } = string.Empty;
        [StringLength(100)] public string? Landmark { get; set; }
        [Required, StringLength(100)] public string City { get; set; } = string.Empty;
        [Required, StringLength(50)] public string State { get; set; } = string.Empty;
        [Required, StringLength(20)] public string Pincode { get; set; } = string.Empty;
        [StringLength(20)] public string Phone { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ApplicationUser? User { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
