namespace Domain;

public class OrderDetails : SelectedProductVariationOption
{
    public Guid CustomerOrderId { get; set; }
    public CustomerOrder CustomerOrder { get; set; }
    public int Quantity { get; set; }
    public Price Price { get; set; }
    public double Discount { get; set; }
    public double Total { get; set; }
}