namespace Domain;

public class ProductVariation
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid VariationId { get; set; }
    public Variation Variation { get; set; }
}