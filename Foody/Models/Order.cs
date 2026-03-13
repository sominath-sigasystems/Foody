using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Order
    {
        [Key] public int Id { get; set; }
        [Required] public string OrderNumber { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? SpecialInstructions { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? PreparedAt { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        // Foreign Keys
        [Required] public string UserId { get; set; } = string.Empty;
        [Required] public int RestaurantId { get; set; }
        [Required] public int AddressId { get; set; }
        public string? DeliveryBoyId { get; set; }

        // Navigation properties
        public ApplicationUser? User { get; set; }
        public Restaurant? Restaurant { get; set; }
        public Address? DeliveryAddress { get; set; }
        public ApplicationUser? DeliveryBoy { get; set; }
        public Payment? Payment { get; set; }  // ← One-to-One, NO PaymentId property
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<OrderStatusHistory>? StatusHistory { get; set; }
        public ICollection<DealUsage>? DealUsages { get; set; }  // ← ADD THIS
    }

}