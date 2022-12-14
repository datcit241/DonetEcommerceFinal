namespace Domain;

public class OrderDetails
{
    public Guid CustomerOrderId { get; set; }
    public CustomerOrder CustomerOrder { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public Rating? Rating { get; set; }
}