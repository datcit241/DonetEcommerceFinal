using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Price
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Amount must not be negative")]
    public double Amount { get; set; }

    public DateTime DateSet { get; set; }
}