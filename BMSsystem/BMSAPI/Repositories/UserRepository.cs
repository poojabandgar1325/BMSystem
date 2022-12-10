using BMSAPI.Data;
using BMSAPI.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(BMSDbContext bMSDbContext)
        {
            this.bMSDbContext = bMSDbContext;

        }
        public BMSDbContext bMSDbContext { get; }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await bMSDbContext.Users.AddAsync(user);
                await bMSDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await bMSDbContext.Users?.FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<bool> UpdateUserAsync(string userName, User user)
        {
           
            var existingUser = await bMSDbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.Name = user.Name;
            existingUser.Password = user.Password;
            existingUser.Address = user.Address;
            existingUser.State = user.State;
            existingUser.Country = user.Country;
            existingUser.Email = user.Email;
            existingUser.PAN = user.PAN;
            existingUser.Contact = user.Contact;
            existingUser.DOB = user.DOB;
            existingUser.AccountType = user.AccountType;

            await bMSDbContext.SaveChangesAsync();

            return true;
        }
    }
}
