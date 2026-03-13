namespace Foody.Utilities
{
    public static class AppRoles
    {
        public const string User = "User";
        public const string DeliveryBoy = "DeliveryBoy";
        public const string RestaurantOwner = "RestaurantOwner";

        public static readonly string[] AllRoles = { User, DeliveryBoy, RestaurantOwner };
    }
}
