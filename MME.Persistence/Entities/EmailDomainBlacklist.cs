namespace MME.Persistence.Entities
{
    public class EmailDomainBlacklist
    {
        public int Id { get; set; }
        public string Domain { get; set; } = string.Empty;
    }

}
