using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Dtos.Accounts;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

namespace api.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly ApplicationDBContext _context;

        public AccountRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateAccountAsync(Account accountModel, UpdateAccountRequestDto accountDto)
        {
            accountModel.Balance = accountDto.Balance;

            await _context.SaveChangesAsync();
        }
    }
}