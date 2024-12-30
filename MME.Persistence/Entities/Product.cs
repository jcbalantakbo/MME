namespace MME.Persistence.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal InterestRate { get; set; }
    public int MinimumTerm { get; set; }
    public bool IsInterestFree { get; set; }
}
