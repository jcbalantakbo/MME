namespace MME.Application.Interfaces;

public interface IMobileBlacklistService
{
    Task<bool> IsBlacklistedAsync(string mobileNumber);
}
