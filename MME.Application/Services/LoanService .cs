using MME.Application.Dtos;
using MME.Application.Interfaces;
using MME.Application.Mappers;
using MME.Common.Models;
using MME.Persistence.Entities;
using MME.Persistence.Interfaces;

public class LoanService : ILoanService
{
    private readonly IMobileBlacklistService _mobileBlacklistService;
    private readonly IEmailBlacklistService _emailBlacklistService;
    private readonly ICalculateRepayments _repaymentCalculator;
    private readonly IUniqueIdentifierGeneratorService _idGenerator;
    private readonly ILoanRepository _loanRepository;
    private readonly IPersonRepository _personRepository;

    public LoanService(
        IMobileBlacklistService mobileBlacklistService,
        IEmailBlacklistService emailBlacklistService,
        ICalculateRepayments repaymentCalculator,
        IUniqueIdentifierGeneratorService idGenerator,
        ILoanRepository loanRepository,
        IPersonRepository personRepository)
    {
        _mobileBlacklistService = mobileBlacklistService;
        _emailBlacklistService = emailBlacklistService;
        _repaymentCalculator = repaymentCalculator;
        _idGenerator = idGenerator;
        _loanRepository = loanRepository;
        _personRepository = personRepository;
    }

    public async Task<Result<string>> CreateLoanAsync(LoanRequestDto request)
    {
        // Blacklist checks
        if (await _mobileBlacklistService.IsBlacklistedAsync(request.Mobile))
        {
            return Result<string>.Failure(
                new ValidationError("Mobile number is blacklisted.", "Mobile")
            );
        }

        if (await _emailBlacklistService.IsEmailBlacklistedAsync(request.Email))
        {
            return Result<string>.Failure(
                new ValidationError("Email domain is blacklisted.", "Email")
            );
        }

        // Validate loan data
        if (request.AmountRequired <= 0)
        {
            return Result<string>.Failure(
                new ValidationError("Loan amount must be greater than zero.", "AmountRequired")
            );
        }

        if (request.Term <= 0)
        {
            return Result<string>.Failure(
                new ValidationError("Loan term must be greater than zero.", "Term")
            );
        }

        // Generate loan ID
        var loanId = _idGenerator.Generate(request.FirstName, request.LastName, request.DateOfBirth,request.Email);

        // Calculate repayments
        var monthlyRepayment = _repaymentCalculator.Calculate(request.AmountRequired, request.Term, request.ProductId);

        // Check if the person already exists by email
        var existingPerson = await _personRepository.GetByEmailAsync(request.Email);

        Person person;
        if (existingPerson != null)
        {
            return Result<string>.Failure(
                new ValidationError("You already have existing loan for this email", "Email")
            );
        }
        else
        {
            // If the person does not exist, create a new person
            person = new Person
            {
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Mobile = request.Mobile,
                Email = request.Email
            };
            // Insert the new person
            await _personRepository.InsertAsync(person);
            await _personRepository.SaveChangesAsync();
        }

        // Create loan entity
        var loan = new Loan
        {
            Id = loanId,
            AmountRequired = request.AmountRequired,
            Term = request.Term,
            PersonId = person.Id,
            ProductId = request.ProductId,
            RepaymentAmount = monthlyRepayment
        };

        // Save loan to the repository
        await _loanRepository.InsertAsync(loan);

        // Save changes
        await _loanRepository.SaveChangesAsync();

        return Result<string>.Success(loanId);
    }

    public async Task<Result<LoanDetailsDto>> GetLoanByIdAsync(string id)
    {
        var loan = await _loanRepository.GetAsync(id, "Product", "Person");

        if (loan == null)
        {
            return Result<LoanDetailsDto>.Failure(
                new NotFoundError("Loan not found.", "Id")
            );
        }

        var loanDetails = loan.ToLoanDetailsDto();
        return Result<LoanDetailsDto>.Success(loanDetails);
    }
}
