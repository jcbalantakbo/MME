namespace MME.Application.Dtos;

public record LoanRequestDto(
    decimal AmountRequired,
    int Term,
    string Title,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Mobile,
    string Email,
    int ProductId
);
