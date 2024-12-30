using MME.Persistence.Context;
using MME.Persistence.Entities;
using MME.Persistence.Interfaces;

namespace MME.Persistence.Repositories;
public class LoanRepository : RepositoryCrud<Loan>, ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
