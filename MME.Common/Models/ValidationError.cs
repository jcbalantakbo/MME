namespace MME.Common.Models;

public class ValidationError : Error
{
    public string Field { get; set; }

    public ValidationError(string description, string field)
        : base(description)
    {
        Field = field;
    }
}

