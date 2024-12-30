using MME.Persistence.Entities;

namespace MME.Persistence.Interfaces;
    /// <summary>
    /// Represents the loan repository.
    /// For Operations exclusive loan entity.
    /// </summary>
public interface IPersonRepository : ICrudRepository<Person>
{
    Task<Person?> GetByEmailAsync(string email);
}

