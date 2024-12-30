using Microsoft.AspNetCore.Mvc;
using MME.Application.Dtos;
using MME.Application.Interfaces;
using MME.Common.Models;

namespace MME.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanRequestService)
        {
            _loanService = loanRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoanAsync([FromBody] LoanRequestDto request)
        {
            var result = await _loanService.CreateLoanAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(new { Error = result.Error.Description, Field = result.Error is ValidationError validationError ? validationError.Field : null });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanAsync(string id)
        {
            var result = await _loanService.GetLoanByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            if (result.Error is NotFoundError)
            {
                return NotFound(new { Error = result.Error.Description });
            }

            return BadRequest(new { Error = result.Error.Description });
        }
    }
}