namespace MME.Persistence.Interfaces;

public interface IMobileBlacklistRepository
{
    Task<HashSet<string>> GetBlacklistedMobileNumbersAsync();
}


