using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Users;

namespace api.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetAllWithAccountAsync();
        Task<User?> GetUserWithAccountAsync(int id);
        Task<bool> CreateUserAsync(User user);
        Task UpdateUserAsync(User userModel, UpdateUserRequestDto userDto);
    }
}