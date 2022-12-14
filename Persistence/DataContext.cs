using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<VariationOption> VariationOptions { get; set; }
    public DbSet<ProductVariation> ProductVariations { get; set; }
    public DbSet<CartDetails> CartDetails { get; set; }
    public DbSet<CouponUser> CouponUsers { get; set; }
    public DbSet<DiscountProduct> DiscountProducts { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProductVariation>(entity => entity.HasKey(
            productVariation => new { productVariation.VariationId, productVariation.ProductId }
        ));
        builder.Entity<ProductVariation>()
            .HasOne(entity => entity.Variation)
            .WithMany(variation => variation.ProductVariations)
            .HasForeignKey(entity => entity.VariationId);
        builder.Entity<ProductVariation>()
            .HasOne(entity => entity.Product)
            .WithMany(product => product.Variations)
            .HasForeignKey(entity => entity.ProductId);

        builder.Entity<CartDetails>(entity => entity.HasKey(
            cartDetails => new { cartDetails.CartId, cartDetails.ProductId }
        ));
        builder.Entity<CartDetails>()
            .HasOne(entity => entity.Cart)
            .WithMany(cart => cart.CartDetails)
            .HasForeignKey(entity => entity.CartId);
        builder.Entity<CartDetails>()
            .HasOne(entity => entity.Product)
            .WithMany(product => product.CartDetails)
            .HasForeignKey(entity => entity.ProductId);

        builder.Entity<CouponUser>(entity => entity.HasKey(
            couponUser => new { couponUser.CouponId, couponUser.UserId }
        ));
        builder.Entity<CouponUser>()
            .HasOne(entity => entity.Coupon)
            .WithMany(coupon => coupon.Users)
            .HasForeignKey(entity => entity.CouponId);
        builder.Entity<CouponUser>()
            .HasOne(entity => entity.User)
            .WithMany(user => user.Coupons)
            .HasForeignKey(entity => entity.UserId);

        builder.Entity<DiscountProduct>(entity => entity.HasKey(
            discountProduct => new { discountProduct.ProductId, discountProduct.DiscountId }
        ));
        builder.Entity<DiscountProduct>()
            .HasOne(entity => entity.Discount)
            .WithMany(discount => discount.AppliedProducts)
            .HasForeignKey(entity => entity.DiscountId);
        builder.Entity<DiscountProduct>()
            .HasOne(entity => entity.Product)
            .WithMany(product => product.Discounts)
            .HasForeignKey(entity => entity.ProductId);

        builder.Entity<OrderDetails>(entity => entity.HasKey(
            orderDetails => new { orderDetails.CustomerOrderId, orderDetails.ProductId }
        ));
        builder.Entity<OrderDetails>()
            .HasOne(entity => entity.CustomerOrder)
            .WithMany(customerOrder => customerOrder.OrderDetails)
            .HasForeignKey(entity => entity.CustomerOrderId);
        builder.Entity<OrderDetails>()
            .HasOne(entity => entity.Product)
            .WithMany(product => product.OrderDetails)
            .HasForeignKey(entity => entity.ProductId);

        builder.Entity<Rating>(entity => entity.HasKey(
            rating => new { rating.OrderDetailsId, rating.UserId }
        ));
        builder.Entity<Rating>()
            .HasOne(entity => entity.OrderDetails)
            .WithOne(entity => entity.Rating).HasForeignKey<OrderDetails>();
    }
}