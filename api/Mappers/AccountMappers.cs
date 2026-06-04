using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Accounts;
using api.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace api.Mappers
{
    public static class AccountMappers
    {
        public static GetAccountDto ToGetAccountDto(this Account account)
        {
            return new GetAccountDto
            {
                UserId = account.UserId,
                Balance = account.Balance,
                AccountNumber = account.AccountNumber
            };
        }
    }
}