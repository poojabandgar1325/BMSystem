using BMSAPI.Data;
using BMSAPI.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly BMSDbContext bMSDbContext;

        public LoanRepository(BMSDbContext bMSDbContext)
        {
            this.bMSDbContext = bMSDbContext;
        }
        public async Task<bool> AddLoanDetailAsync(Loan loanDetail)
        {
            try
            {
                await bMSDbContext.Loans.AddAsync(loanDetail);
                await bMSDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Loan>> GetAllLoanAsync()
        {
            return await bMSDbContext.Loans.ToListAsync();
        }

        public async Task<List<Loan>> GetAllLoanByUsernameAsync(string userName)
        {
            return await bMSDbContext.Loans?.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<Loan> GetLoanAsync(int loanId)
        {
            return await bMSDbContext.Loans?.FirstOrDefaultAsync(x => x.LoanId == loanId);
        }

        public async Task<bool> UpdateStatusAsync(int loanId, string status)
        {
            var existingUser = await bMSDbContext.Loans.FirstOrDefaultAsync(x => x.LoanId == loanId);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Status = status;


            await bMSDbContext.SaveChangesAsync();

            return true;
        }
    }
}
