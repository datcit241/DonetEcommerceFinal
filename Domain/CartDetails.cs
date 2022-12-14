namespace Domain;

public class CartDetails : SelectedProductVariationOption
{
    public string CartId { set; get; }
    public Cart Cart { get; set; }
    public int Quantity { get; set; }
}