namespace MME.Application.Interfaces;

public interface IUniqueIdentifierGeneratorService
{
    string Generate(string firstName, string lastName, DateTime dateOfBirth, string email);
}
