using MME.Application.Dtos;
using MME.Common.Models;

namespace MME.Application.Interfaces
{
    public interface ILoanService
    {
        Task<Result<string>> CreateLoanAsync(LoanRequestDto dto);
        Task<Result<LoanDetailsDto>> GetLoanByIdAsync(string Id);
    }
}
