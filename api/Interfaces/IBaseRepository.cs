using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Delete(T data);
        Task<bool> SaveChangesAsync();
    }
}