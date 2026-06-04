using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos.Users;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.Include(a => a.Accounts).ToListAsync();
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users.Include(a => a.Accounts).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if(user == null)
                return false;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task UpdateUserAsync(User userModel, UpdateUserRequestDto userDto)
        {
            userModel.FirstName = userDto.FirstName;
            userModel.LastName = userDto.LastName;
            userModel.Age = userDto.Age;
            userModel.Email = userDto.Email;
            userModel.PhoneNumber = userDto.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}