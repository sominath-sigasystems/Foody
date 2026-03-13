namespace Foody.Utilities
{
    public enum PaymentMethodType
    {
        CreditCard = 1,
        DebitCard = 2,
        UPI = 3,
        NetBanking = 4,
        Wallet = 5,
        CashOnDelivery = 6
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Success = 2,
        Failed = 3,
        Refunded = 4,
        Cancelled = 5
    }
    public enum DealType { Percentage, Fixed, BOGO, Combo, FreeDelivery, FreeItem }
}
