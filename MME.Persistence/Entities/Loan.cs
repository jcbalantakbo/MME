namespace MME.Persistence.Entities;

public class Loan
{
    public string Id { get; set; } = string.Empty;
    public decimal AmountRequired { get; set; }
    public int Term { get; set; }
    public int ProductId { get; set; }  
    public Product Product { get; set; } = null!;
    public decimal RepaymentAmount { get; set; }

    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
}
