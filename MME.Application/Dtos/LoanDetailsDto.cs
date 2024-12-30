namespace MME.Application.Dtos;

public record LoanDetailsDto(
    string Id,
    decimal AmountRequired,
    int Term,
    decimal RepaymentAmount,
    string ProductName,
    PersonDto Person
);