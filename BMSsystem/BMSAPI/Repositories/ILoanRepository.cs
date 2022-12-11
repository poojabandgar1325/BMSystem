using BMSAPI.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan> GetLoanAsync(int loanId);
        Task<List<Loan>> GetAllLoanByUsernameAsync(string userName);
        Task<List<Loan>> GetAllLoanAsync();
        Task<bool> UpdateStatusAsync(int loanId, string status);
        Task<bool> AddLoanDetailAsync(Loan loanDetail);
    }
}
