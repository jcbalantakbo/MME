
using MME.Common.Models;

namespace MME.Application.Models
{
    public class LoanError : Error
    {
        public LoanError(string description) : base(description) { }
    }

}
