using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<Price> Prices { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be not be negative")]
    public int Quantity { get; set; }

    public DateTime DateAdded { get; set; }

    public ICollection<DiscountProduct> Discounts { get; set; }
    public ProductStatus Status { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public decimal Revenue { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public ICollection<CartDetails> CartDetails { get; set; }
    public ICollection<ProductVariation> Variations { get; set; }
    public ICollection<ProductCategory> Categories { get; set; }
}