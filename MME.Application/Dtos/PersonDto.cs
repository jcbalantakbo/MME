namespace MME.Application.Dtos;

public record PersonDto(
    string Title,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Mobile,
    string Email
);
