using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Helpers;
using api.Dtos.Users;

namespace api.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetAllWithAccountAsync(QueryObject queryObject);
        Task<User?> GetUserWithAccountAsync(int id);
    }
}