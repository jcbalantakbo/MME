namespace MME.Application.Interfaces;

public interface ICalculateRepayments
{
    decimal Calculate(decimal amount, int term, int productId);
}
