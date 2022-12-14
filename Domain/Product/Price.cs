namespace Domain;

public class Price
{
    public Guid Id { get; set; }
    public Product Product { get; set; }
    public double Amount { get; set; }
    public DateTime DateSet { get; set; }
}