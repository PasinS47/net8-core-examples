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
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;

        public AccountRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetAccountAsync(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            if(account == null)
                return false;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task UpdateAccountAsync(Account accountModel, UpdateAccountRequestDto accountDto)
        {
            accountModel.Balance = accountDto.Balance;

            await _context.SaveChangesAsync();
        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}