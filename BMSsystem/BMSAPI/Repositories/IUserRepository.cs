using BMSAPI.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMSAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userName);
       
        Task<bool> AddUserAsync(User userDetail);

        Task<bool> UpdateUserAsync(string userName, User userDetail);


        Task<int> UserCredentialsAsync(string userName, string password);
    }
}
