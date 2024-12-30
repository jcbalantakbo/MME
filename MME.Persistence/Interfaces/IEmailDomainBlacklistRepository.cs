namespace MME.Persistence.Interfaces;


public interface IEmailDomainBlacklistRepository
{
    Task<HashSet<string>> GetBlacklistedDomainsAsync();
}



