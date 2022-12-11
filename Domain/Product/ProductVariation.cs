namespace Domain;

public class ProductVariation
{
    public Product Product { get; set; }
    public Variation Variation { get; set; }
    public int Quantity { get; set; }
}