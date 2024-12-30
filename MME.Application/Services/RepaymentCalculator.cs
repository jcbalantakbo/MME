using MME.Application.Interfaces;

namespace MME.Application.Services;

public class RepaymentCalculator : ICalculateRepayments
{
    public decimal Calculate(decimal amount, int term, int productId)
    {
        // Initialize interest rate to zero
        decimal interestRate = 0;
        decimal monthlyRepayment = 0;

        switch (productId)
        {
            case 1:
                // Product A: Interest-free loan
                return amount / term; 

            case 2:
                // Product B: First 2 months interest-free
                if (term > 2)
                {
                    // First 2 months interest-free, apply interest after that
                    decimal interestFreeAmount = amount * 0;
                    decimal remainingAmount = amount;

                    // Assumed a 5% annual interest rate for the rest of the term (after first 2 months), instructions didn't specify interest
                    interestRate = 0.05m; 
                    decimal remainingRepaymentAmount = remainingAmount * interestRate / 12;
                   
                    monthlyRepayment = (remainingAmount + remainingRepaymentAmount) / term;
                }
                break;

            case 3:
                // Product C: Not interest-free
                interestRate = 0.1m; // Assumed 10% annual interest rate, instructions didn't specify interest
                decimal monthlyInterestRate = interestRate / 12;
                monthlyRepayment = (amount * monthlyInterestRate) / (1 - (decimal)Math.Pow(1 + (double)monthlyInterestRate, -term));
                break;
        }

        return monthlyRepayment;
    }
}