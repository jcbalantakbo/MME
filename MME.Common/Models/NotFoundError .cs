namespace MME.Common.Models;

public class NotFoundError : Error
{
    public string ResourceName { get; set; }

    public NotFoundError(string description, string resourceName)
        : base(description)
    {
        ResourceName = resourceName;
    }
}
