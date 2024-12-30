using System;
using Xunit;
using MME.Application.Services;

namespace MME.Application.Services.Tests
{
    public class RepaymentCalculatorTests
    {
        private readonly RepaymentCalculator _repaymentCalculator;

        public RepaymentCalculatorTests()
        {
            // Initialize the service before each test
            _repaymentCalculator = new RepaymentCalculator();
        }

        [Fact]
        public void Calculate_ShouldReturnCorrectRepayment_ForInterestFreeLoan()
        {
            // Arrange
            decimal amount = 1200m;
            int term = 12; // 12 months
            int productId = 1; // Product A (Interest-free loan)

            // Act
            var result = _repaymentCalculator.Calculate(amount, term, productId);

            // Assert
            var expectedRepayment = amount / term; // No interest, just divide by the term
            Assert.Equal(expectedRepayment, result); // Repayment should be the amount divided by the term
        }

        [Fact]
        public void Calculate_ShouldReturnCorrectRepayment_ForLoanWithInterestAfterTwoMonths()
        {
            // Arrange
            decimal amount = 1200m;
            int term = 12; // 12 months
            int productId = 2; // Product B (Interest-free for first 2 months, interest applied after)

            // Act
            var result = _repaymentCalculator.Calculate(amount, term, productId);

            // Assert
            // First 2 months are interest-free. After 2 months, apply 5% annual interest
            decimal interestRate = 0.05m; // 5% annual interest rate
            decimal remainingAmount = amount; // No reduction in the amount during the first 2 months
            decimal remainingRepaymentAmount = remainingAmount * interestRate / 12; // Monthly interest
            decimal monthlyRepayment = (remainingAmount + remainingRepaymentAmount) / term;

            Assert.Equal(monthlyRepayment, result);
        }

        [Fact]
        public void Calculate_ShouldReturnCorrectRepayment_ForLoanWithInterestFromTheStart()
        {
            // Arrange
            decimal amount = 1200m;
            int term = 12; // 12 months
            int productId = 3; // Product C (Interest from the start)

            // Act
            var result = _repaymentCalculator.Calculate(amount, term, productId);

            // Assert
            // Apply 10% annual interest rate, divide by 12 for monthly interest
            decimal interestRate = 0.1m; // 10% annual interest rate
            decimal monthlyInterestRate = interestRate / 12; // Monthly interest rate
            decimal monthlyRepayment = (amount * monthlyInterestRate) / (1 - (decimal)Math.Pow(1 + (double)monthlyInterestRate, -term));

            Assert.Equal(monthlyRepayment, result); // Repayment should match the calculated amount
        }


    }
}
