namespace MME.Application.Interfaces;

public interface IEmailBlacklistService
{
    Task<bool> IsEmailBlacklistedAsync(string email);
}
