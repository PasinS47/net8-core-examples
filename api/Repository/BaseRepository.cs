using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using api.Data;
using api.Helpers;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(QueryObject queryObject)
        {
            var datas = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                var sortDirection = queryObject.IsDescending ? "descending" : "ascending";
                datas = datas.OrderBy($"{queryObject.SortBy} {sortDirection}");
            }

            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            return await _dbSet.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> CreateAsync(T data)
        {
            if(data == null)
                return false;

            await _dbSet.AddAsync(data);

            return await _context.SaveChangesAsync() > 0;
        }

        public void Delete(T data)
        {
            _dbSet.Remove(data);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}