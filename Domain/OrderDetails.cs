namespace Domain.UserOrder;

public class OrderDetails
{
    public CustomerOrder Order { get; set; }
    public Product.Product Product { get; set; }
}