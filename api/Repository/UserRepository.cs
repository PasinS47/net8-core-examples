using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos.Users;
using Microsoft.EntityFrameworkCore;
using api.Helpers;

namespace api.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllWithAccountAsync(QueryObject queryObject)
        {
            var datas = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                var sortDirection = queryObject.IsDescending ? "descending" : "ascending";
                datas = datas.OrderBy($"{queryObject.SortBy} {sortDirection}");
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            return await datas.Skip(skipNumber).Take(queryObject.PageSize).Include(a => a.Accounts).ToListAsync();
        }

        public async Task<User?> GetUserWithAccountAsync(int id)
        {
            return await _context.Users.Include(a => a.Accounts).FirstOrDefaultAsync(i => i.Id == id);
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
    }
}