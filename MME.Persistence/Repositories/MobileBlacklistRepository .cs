using Microsoft.EntityFrameworkCore;
using MME.Persistence.Context;
using MME.Persistence.Interfaces;

namespace MME.Persistence.Repositories;

public class MobileBlacklistRepository : IMobileBlacklistRepository
{
    private readonly ApplicationDbContext _context;

    public MobileBlacklistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetBlacklistedMobileNumbersAsync()
    {
        return await _context.MobileBlacklist
            .Select(m => m.MobileNumber)
            .ToHashSetAsync();
    }
}

