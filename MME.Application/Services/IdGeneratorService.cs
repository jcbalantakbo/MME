using MME.Application.Interfaces;

namespace MME.Application.Services;

public class IdGeneratorService : IUniqueIdentifierGeneratorService
{
    public string Generate(string firstName, string lastName, DateTime dateOfBirth, string email)
    {

        var input = $"{firstName.ToLowerInvariant()}_{lastName.ToLowerInvariant()}_{dateOfBirth:yyyyMMdd}_{email.ToLowerInvariant()}";

        var hash = System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));

        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
