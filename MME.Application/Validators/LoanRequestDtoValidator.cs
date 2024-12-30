using FluentValidation;
using MME.Application.Dtos;
using MME.Application.Helpers;

namespace MME.Application.Validators;

public class LoanRequestDtoValidator : AbstractValidator<LoanRequestDto>
{
    private static readonly HashSet<string> BlacklistedDomains = new() { "spam.com", "blacklist.com" };

    public LoanRequestDtoValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .Must(dateOfBirth => ValidationHelper.BeAtLeast18YearsOld(dateOfBirth))
            .WithMessage("Applicant must be at least 18 years old.");

        RuleFor(x => x.Email)
            .Must(email => !ValidationHelper.IsBlacklistedDomain(email, BlacklistedDomains))
            .WithMessage("Email domain is blacklisted.");
    }
}
