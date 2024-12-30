using System;
using Xunit;

namespace MME.Application.Services.Tests
{
    public class IdGeneratorServiceTests
    {
        private readonly IdGeneratorService _idGeneratorService;

        public IdGeneratorServiceTests()
        {
            // Initialize the service before each test
            _idGeneratorService = new IdGeneratorService();
        }

        [Fact]
        public void Generate_ShouldReturnSameId_ForSameInput()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            string email = "john.doe@example.com";

            // Act
            var id1 = _idGeneratorService.Generate(firstName, lastName, dateOfBirth, email);
            var id2 = _idGeneratorService.Generate(firstName, lastName, dateOfBirth, email);

            // Assert
            Assert.Equal(id1, id2); // Generated IDs should be the same for identical input.
        }

        [Fact]
        public void Generate_ShouldReturnDifferentId_ForDifferentInput()
        {
            // Arrange
            string firstName1 = "John";
            string lastName1 = "Doe";
            DateTime dateOfBirth1 = new DateTime(1990, 1, 1);
            string email1 = "john.doe@example.com";

            string firstName2 = "Jane";
            string lastName2 = "Smith";
            DateTime dateOfBirth2 = new DateTime(1992, 5, 12);
            string email2 = "jane.smith@example.com";

            // Act
            var id1 = _idGeneratorService.Generate(firstName1, lastName1, dateOfBirth1, email1);
            var id2 = _idGeneratorService.Generate(firstName2, lastName2, dateOfBirth2, email2);

            // Assert
            Assert.NotEqual(id1, id2); // Generated IDs should be different for different input.
        }

        [Fact]
        public void Generate_ShouldReturnSameId_ForIdenticalFormattedInput()
        {
            // Arrange
            string firstName = "john";
            string lastName = "doe";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            string email = "john.doe@example.com";

            // Act
            var id1 = _idGeneratorService.Generate(firstName.ToUpper(), lastName.ToUpper(), dateOfBirth, email.ToUpper());
            var id2 = _idGeneratorService.Generate(firstName.ToLower(), lastName.ToLower(), dateOfBirth, email.ToLower());

            // Assert
            Assert.Equal(id1, id2); // Generated IDs should be the same for input with different casing.
        }

        [Fact]
        public void Generate_ShouldReturnSameId_ForEmptyFields()
        {
            // Arrange
            string firstName = "";
            string lastName = "";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            string email = "";

            // Act
            var id1 = _idGeneratorService.Generate(firstName, lastName, dateOfBirth, email);
            var id2 = _idGeneratorService.Generate(firstName, lastName, dateOfBirth, email);

            // Assert
            Assert.Equal(id1, id2); // Generated IDs should be the same when input fields are empty.
        }
    }
}
