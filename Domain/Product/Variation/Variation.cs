namespace Domain;

public class Variation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<VariationOption> VariationOptions { get; set; }
    public ICollection<ProductVariation> ProductVariations { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
    public ICollection<CartDetails> CartDetails { get; set; }
}