using Microsoft.EntityFrameworkCore;
using MME.Persistence.Context;
using MME.Persistence.Entities;
using MME.Persistence.Interfaces;

namespace MME.Persistence.Repositories;

public class EmailDomainBlacklistRepository : IEmailDomainBlacklistRepository
{
    private readonly ApplicationDbContext _context;

    public EmailDomainBlacklistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetBlacklistedDomainsAsync()
    {
        return await _context.EmailDomainBlacklist
            .Select(e => e.Domain)
            .ToHashSetAsync();
    }
}

