using MME.Application.Interfaces;
using MME.Persistence.Interfaces;

namespace MME.Application.Services;

public class MobileBlacklistService : IMobileBlacklistService
{
    private readonly IMobileBlacklistRepository _mobileBlacklistRepository;

    public MobileBlacklistService(IMobileBlacklistRepository mobileBlacklistRepository)
    {
        _mobileBlacklistRepository = mobileBlacklistRepository;
    }

    public async Task<bool> IsBlacklistedAsync(string mobile)
    {
        var blacklistedMobileNumbers = await _mobileBlacklistRepository.GetBlacklistedMobileNumbersAsync();
        return blacklistedMobileNumbers.Contains(mobile);
    }
}

