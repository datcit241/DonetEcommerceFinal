using System.ComponentModel.DataAnnotations;

namespace Domain;

public class OrderDetails : SelectedProductVariationOption
{
    public Guid CustomerOrderId { get; set; }
    public CustomerOrder CustomerOrder { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    public Price Price { get; set; }
    public double Discount { get; set; }
    public double Total { get; set; }
}