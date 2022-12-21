using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Discount
{
    public Guid Id { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Amount must not be negative")]
    public double Amount { get; set; }

    public DiscountStatus DiscountStatus { get; set; }
    public ICollection<DiscountProduct> AppliedProducts { get; set; }
}