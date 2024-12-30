namespace MME.Common.Models;

public abstract class Error
{
    public string Description { get; set; }

    protected Error(string description)
    {
        Description = description;
    }
}

