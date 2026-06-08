using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(QueryObject queryObject);
        Task<T?> GetByIdAsync(int id);
        Task<bool> CreateAsync(T data);
        void Delete(T data);
        Task<bool> SaveChangesAsync();
    }
}