namespace Domain;

public class VariationOption
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Charge { get; set; }
    public Variation Variation { get; set; }
}