using Foody.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foody.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewReply> ReviewReplies { get; set; }
        public DbSet<RestaurantTiming> RestaurantTimings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<MenuItemTag> MenuItemTags { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealCategory> DealCategories { get; set; }
        public DbSet<DealMenuItem> DealMenuItems { get; set; }
        public DbSet<DealUsage> DealUsages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Restaurant → Category → MenuItem
            builder.Entity<Restaurant>()
                .HasMany(r => r.Categories)
                .WithOne(c => c.Restaurant)
                .HasForeignKey(c => c.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Category>()
                .HasMany(c => c.MenuItems)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order → OrderItem
            builder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // FIX: Order → Address (prevent multiple cascade paths)
            builder.Entity<Order>()
                .HasOne(o => o.DeliveryAddress)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payment ↔ Order
            builder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser relationships
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.PlacedOrders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.AssignedDeliveries)
                .WithOne(o => o.DeliveryBoy)
                .HasForeignKey(o => o.DeliveryBoyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnedRestaurants)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cart → CartItem
            builder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Review
            builder.Entity<Review>()
                .HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reviews)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.MenuItem)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Deal relationships
            builder.Entity<Deal>()
                .HasOne(d => d.Restaurant)
                .WithMany(r => r.Deals)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DealCategory>().HasKey(dc => dc.Id);

            builder.Entity<DealCategory>()
                .HasOne(dc => dc.Deal)
                .WithMany(d => d.DealCategories)
                .HasForeignKey(dc => dc.DealId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DealCategory>()
                .HasOne(dc => dc.Category)
                .WithMany(c => c.DealCategories)
                .HasForeignKey(dc => dc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DealMenuItem>().HasKey(dm => dm.Id);

            builder.Entity<DealMenuItem>()
                .HasOne(dm => dm.Deal)
                .WithMany(d => d.DealMenuItems)
                .HasForeignKey(dm => dm.DealId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DealMenuItem>()
                .HasOne(dm => dm.MenuItem)
                .WithMany(m => m.DealMenuItems)
                .HasForeignKey(dm => dm.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DealUsage>()
                .HasIndex(du => new { du.DealId, du.UserId })
                .IsUnique();

            builder.Entity<DealUsage>()
                .HasOne(du => du.Deal)
                .WithMany(d => d.DealUsages)
                .HasForeignKey(du => du.DealId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DealUsage>()
                .HasOne(du => du.User)
                .WithMany()
                .HasForeignKey(du => du.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DealUsage>()
                .HasOne(du => du.Order)
                .WithMany(o => o.DealUsages)
                .HasForeignKey(du => du.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // MenuItem ↔ Tag
            builder.Entity<MenuItemTag>().HasKey(mt => mt.Id);

            builder.Entity<MenuItemTag>()
                .HasOne(mt => mt.MenuItem)
                .WithMany(m => m.MenuItemTags)
                .HasForeignKey(mt => mt.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MenuItemTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.MenuItemTags)
                .HasForeignKey(mt => mt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // Decimal precision
            builder.Entity<Address>().Property(a => a.Latitude).HasPrecision(9, 6);
            builder.Entity<Address>().Property(a => a.Longitude).HasPrecision(9, 6);

            builder.Entity<Restaurant>().Property(r => r.DeliveryFee).HasPrecision(18, 2);
            builder.Entity<Restaurant>().Property(r => r.MinimumOrder).HasPrecision(18, 2);
            builder.Entity<Restaurant>().Property(r => r.Rating).HasPrecision(3, 2);

            builder.Entity<MenuItem>().Property(m => m.Price).HasPrecision(18, 2);
            builder.Entity<MenuItem>().Property(m => m.DiscountedPrice).HasPrecision(18, 2);

            builder.Entity<Order>().Property(o => o.SubTotal).HasPrecision(18, 2);
            builder.Entity<Order>().Property(o => o.Tax).HasPrecision(18, 2);
            builder.Entity<Order>().Property(o => o.DeliveryFee).HasPrecision(18, 2);
            builder.Entity<Order>().Property(o => o.Discount).HasPrecision(18, 2);
            builder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);

            builder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);

            builder.Entity<Payment>().Property(p => p.Amount).HasPrecision(18, 2);

            builder.Entity<Deal>().Property(d => d.DiscountValue).HasPrecision(18, 2);
            builder.Entity<Deal>().Property(d => d.MaxDiscountAmount).HasPrecision(18, 2);
            builder.Entity<Deal>().Property(d => d.MinOrderValue).HasPrecision(18, 2);

            builder.Entity<DealCategory>().Property(dc => dc.OverrideDiscountValue).HasPrecision(18, 2);
            builder.Entity<DealMenuItem>().Property(dm => dm.OverrideDiscountValue).HasPrecision(18, 2);
            builder.Entity<DealUsage>().Property(du => du.DiscountApplied).HasPrecision(18, 2);

            builder.Entity<PromoCode>().Property(p => p.Value).HasPrecision(18, 2);
            builder.Entity<PromoCode>().Property(p => p.MinOrderValue).HasPrecision(18, 2);
            builder.Entity<PromoCode>().Property(p => p.MaxDiscount).HasPrecision(18, 2);

            // Indexes
            builder.Entity<Restaurant>().HasIndex(r => r.City);
            builder.Entity<Restaurant>().HasIndex(r => new { r.City, r.IsOpen });

            builder.Entity<MenuItem>().HasIndex(m => m.IsAvailable);

            builder.Entity<Order>().HasIndex(o => o.UserId);
            builder.Entity<Order>().HasIndex(o => o.RestaurantId);
            builder.Entity<Order>().HasIndex(o => o.Status);

            builder.Entity<PromoCode>().HasIndex(p => p.Code).IsUnique();

            builder.Entity<Deal>().HasIndex(d => d.IsActive);
            builder.Entity<Deal>().HasIndex(d => d.IsFlashDeal);
            builder.Entity<Deal>().HasIndex(d => new { d.StartDate, d.EndDate });
            builder.Entity<Deal>().HasIndex(d => d.RestaurantId);
        }
    }
}