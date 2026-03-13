namespace Foody.Models
{
    public enum OrderStatus
    {
        Pending, Confirmed, Preparing, ReadyForPickup,
        PickedUp, OutForDelivery, Delivered, Cancelled, Refunded
    }
}
