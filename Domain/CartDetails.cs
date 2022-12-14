namespace Domain;

public class CartDetails
{
    public string CartId { set; get; }
    public Cart Cart { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}