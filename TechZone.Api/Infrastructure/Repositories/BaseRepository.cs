using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.Consts;
using TechZone.Domain.Interfaces;
using TechZone.Infrastructure.Application;

namespace TechZone.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Implement IBaseRepository methods
        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // This assumes your entities have an "Id" property
            // You might need to adjust this based on your actual primary key
            return await query.FirstOrDefaultAsync(e => Microsoft.EntityFrameworkCore.EF.Property<int>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<T>> WhereAsync(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy orderDirection = OrderBy.Ascending,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Where(criteria);

            if (orderBy != null)
            {
                query = orderDirection == OrderBy.Ascending
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null)
        {
            if (criteria == null)
            {
                return await _dbSet.CountAsync();
            }

            return await _dbSet.CountAsync(criteria);
        }

        // Keep your existing additional methods as they are useful
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }



        // Extension methods to support LINQ operations
        public async Task<List<T>> ToListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.MaxAsync(selector);
        }

        public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
        {
            return await _dbSet.MinAsync(selector);
        }

        // Fixed SumAsync and AverageAsync methods
        public async Task<int> SumAsync(Expression<Func<T, int>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<long> SumAsync(Expression<Func<T, long>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<double> SumAsync(Expression<Func<T, double>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<float> SumAsync(Expression<Func<T, float>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<int?> SumAsync(Expression<Func<T, int?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<long?> SumAsync(Expression<Func<T, long?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<double?> SumAsync(Expression<Func<T, double?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        public async Task<float?> SumAsync(Expression<Func<T, float?>> selector)
        {
            return await _dbSet.SumAsync(selector);
        }

        // AverageAsync methods
        public async Task<double> AverageAsync(Expression<Func<T, int>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double> AverageAsync(Expression<Func<T, long>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double> AverageAsync(Expression<Func<T, double>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<float> AverageAsync(Expression<Func<T, float>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double?> AverageAsync(Expression<Func<T, int?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double?> AverageAsync(Expression<Func<T, long?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<double?> AverageAsync(Expression<Func<T, double?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

        public async Task<float?> AverageAsync(Expression<Func<T, float?>> selector)
        {
            return await _dbSet.AverageAsync(selector);
        }

    }
}