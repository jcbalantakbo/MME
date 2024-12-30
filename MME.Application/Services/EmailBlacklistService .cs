using MME.Application.Interfaces;
using MME.Persistence.Interfaces;

namespace MME.Application.Services;

public class EmailBlacklistService : IEmailBlacklistService
{
    private readonly IEmailDomainBlacklistRepository _emailDomainBlacklistRepository;

    public EmailBlacklistService(IEmailDomainBlacklistRepository emailDomainBlacklistRepository)
    {
        _emailDomainBlacklistRepository = emailDomainBlacklistRepository;
    }

    public async Task<bool> IsEmailBlacklistedAsync(string email)
    {
        var blacklistedDomains = await _emailDomainBlacklistRepository.GetBlacklistedDomainsAsync();
        var domain = email.Split('@').LastOrDefault();
        return domain != null && blacklistedDomains.Contains(domain);
    }
}
