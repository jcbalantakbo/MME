using MME.Application.Dtos;
using MME.Application.Interfaces;
using MME.Common.Models;
using MME.Persistence.Entities;
using MME.Persistence.Interfaces;
using Moq;

namespace MME.Services.Tests
{
    public class LoanServiceTests
    {
        private readonly Mock<IMobileBlacklistService> _mockMobileBlacklistService;
        private readonly Mock<IEmailBlacklistService> _mockEmailBlacklistService;
        private readonly Mock<ICalculateRepayments> _mockRepaymentCalculator;
        private readonly Mock<IUniqueIdentifierGeneratorService> _mockIdGenerator;
        private readonly Mock<ILoanRepository> _mockLoanRepository;
        private readonly Mock<IPersonRepository> _mockPersonRepository;
        private readonly LoanService _loanService;

        public LoanServiceTests()
        {
            _mockMobileBlacklistService = new Mock<IMobileBlacklistService>();
            _mockEmailBlacklistService = new Mock<IEmailBlacklistService>();
            _mockRepaymentCalculator = new Mock<ICalculateRepayments>();
            _mockIdGenerator = new Mock<IUniqueIdentifierGeneratorService>();
            _mockLoanRepository = new Mock<ILoanRepository>();
            _mockPersonRepository = new Mock<IPersonRepository>();

            _loanService = new LoanService(
                _mockMobileBlacklistService.Object,
                _mockEmailBlacklistService.Object,
                _mockRepaymentCalculator.Object,
                _mockIdGenerator.Object,
                _mockLoanRepository.Object,
                _mockPersonRepository.Object
            );
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldFail_WhenMobileIsBlacklisted()
        {
            // Arrange
            var loanRequest = new LoanRequestDto(
                1000, 12, "Mr.", "John", "Doe", new DateTime(2000, 1, 1), "09123456789", "john.doe@example.com", 1
            );

            _mockMobileBlacklistService.Setup(service => service.IsBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _loanService.CreateLoanAsync(loanRequest);

            // Assert
            var error = result.Error;
            Assert.IsType<ValidationError>(error); // Check the concrete error type
            Assert.Equal("Mobile number is blacklisted.", error.Description); // Check the error description
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldFail_WhenEmailIsBlacklisted()
        {
            // Arrange
            var loanRequest = new LoanRequestDto(
                1000, 12, "Mr.", "John", "Doe", new DateTime(2000, 1, 1), "09123456789", "john.doe@spam.com", 1
            );

            _mockMobileBlacklistService.Setup(service => service.IsBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockEmailBlacklistService.Setup(service => service.IsEmailBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _loanService.CreateLoanAsync(loanRequest);

            // Assert
            var error = result.Error;
            Assert.IsType<ValidationError>(error);
            Assert.Equal("Email domain is blacklisted.", error.Description);
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldFail_WhenLoanAmountIsZeroOrNegative()
        {
            // Arrange
            var loanRequest = new LoanRequestDto(
                0, 12, "Mr.", "John", "Doe", new DateTime(2000, 1, 1), "09123456789", "john.doe@example.com", 1
            );

            _mockMobileBlacklistService.Setup(service => service.IsBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockEmailBlacklistService.Setup(service => service.IsEmailBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _loanService.CreateLoanAsync(loanRequest);

            // Assert
            var error = result.Error;
            Assert.IsType<ValidationError>(error);
            Assert.Equal("Loan amount must be greater than zero.", error.Description);
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldFail_WhenLoanTermIsZeroOrNegative()
        {
            // Arrange
            var loanRequest = new LoanRequestDto(
                1000, 0, "Mr.", "John", "Doe", new DateTime(2000, 1, 1), "09123456789", "john.doe@example.com", 1
            );

            _mockMobileBlacklistService.Setup(service => service.IsBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockEmailBlacklistService.Setup(service => service.IsEmailBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _loanService.CreateLoanAsync(loanRequest);

            // Assert
            var error = result.Error;
            Assert.IsType<ValidationError>(error);
            Assert.Equal("Loan term must be greater than zero.", error.Description);
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldFail_WhenPersonAlreadyExists()
        {
            // Arrange
            var loanRequest = new LoanRequestDto(
                1000, 12, "Mr.", "John", "Doe", new DateTime(2000, 1, 1), "09123456789", "john.doe@example.com", 1
            );

            var existingPerson = new Person { Email = "john.doe@example.com" };
            _mockPersonRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(existingPerson);

            // Act
            var result = await _loanService.CreateLoanAsync(loanRequest);

            // Assert
            var error = result.Error;
            Assert.IsType<ValidationError>(error);
            Assert.Equal("You already have existing loan for this email", error.Description);
        }

        [Fact]
        public async Task CreateLoanAsync_ShouldSucceed_WhenValidRequest()
        {
            // Arrange
            var loanRequest = new LoanRequestDto(
                1000, 12, "Mr.", "John", "Doe", new DateTime(2000, 1, 1), "09123456789", "john.doe@example.com", 1
            );

            _mockMobileBlacklistService.Setup(service => service.IsBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockEmailBlacklistService.Setup(service => service.IsEmailBlacklistedAsync(It.IsAny<string>())).ReturnsAsync(false);
            _mockPersonRepository.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Person)null);
            _mockRepaymentCalculator.Setup(calc => calc.Calculate(It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>())).Returns(100);
            _mockIdGenerator.Setup(gen => gen.Generate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns("loanId123");

            // Act
            var result = await _loanService.CreateLoanAsync(loanRequest);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("loanId123", result.Data);
        }
    }
}
