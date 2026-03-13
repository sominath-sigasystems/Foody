namespace Foody.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Restaurant> TopRestaurants { get; set; }
        public IEnumerable<MenuItem> PopularFoods { get; set; }
        public IEnumerable<Deal> FeaturedDeals { get; set; }
        public IEnumerable<Deal> FlashDeals { get; set; }
    }
}
