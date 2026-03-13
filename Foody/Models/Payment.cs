using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class Payment
    {
        [Key] public int Id { get; set; }  // ✅ Auto-generated (remove DatabaseGenerated)

        [Required] public string TransactionId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public PaymentMethodType Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? GatewayResponse { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        // ✅ Foreign Key to Order (REQUIRED)
        [Required] public int OrderId { get; set; }

        // ✅ Navigation with ForeignKey attribute
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }

    public enum PaymentMethodType { CreditCard, DebitCard, UPI, NetBanking, Wallet, CashOnDelivery }
    public enum PaymentStatus { Pending, Success, Failed, Refunded, Cancelled }
}