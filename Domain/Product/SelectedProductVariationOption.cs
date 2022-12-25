namespace Domain;

public class SelectedProductVariationOption
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid VariationOptionId { get; set; }
    public VariationOption VariationOption { get; set; }
}