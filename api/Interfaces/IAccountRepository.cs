using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Accounts;

namespace api.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        Task<bool> CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account accountModel, UpdateAccountRequestDto accountDto);
    }
}