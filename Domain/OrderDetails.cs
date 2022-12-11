namespace Domain;

public class OrderDetails
{
    public CustomerOrder Order { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
}