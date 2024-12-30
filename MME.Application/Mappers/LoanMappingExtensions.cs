using MME.Application.Dtos;
using MME.Persistence.Entities;

namespace MME.Application.Mappers;

public static class LoanMappingExtensions
{
    public static LoanDetailsDto ToLoanDetailsDto(this Loan loan)
    {
        if (loan == null) throw new ArgumentNullException(nameof(loan));

        return new LoanDetailsDto(
            Id: loan.Id,
            AmountRequired: loan.AmountRequired,
            Term: loan.Term,
            RepaymentAmount: loan.RepaymentAmount,
            ProductName: loan.Product?.Name ?? "Unknown",
            Person: new PersonDto(
                Title: loan.Person.Title,
                FirstName: loan.Person.FirstName,
                LastName: loan.Person.LastName,
                DateOfBirth: loan.Person.DateOfBirth,
                Mobile: loan.Person.Mobile,
                Email: loan.Person.Email
            )
        );
    }
}
