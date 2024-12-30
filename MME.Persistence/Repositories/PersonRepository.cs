using Microsoft.EntityFrameworkCore;
using MME.Persistence.Context;
using MME.Persistence.Entities;
using MME.Persistence.Interfaces;

namespace MME.Persistence.Repositories;
public class PersonRepository : RepositoryCrud<Person>, IPersonRepository
{
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Person?> GetByEmailAsync(string email)
    {
        return await _context.Set<Person>().FirstOrDefaultAsync(p => p.Email == email);
    }

}
