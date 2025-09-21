using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechZone.Core.Consts;
using TechZone.Core.Entities;
using TechZone.Core.Interfaces;
using TechZone.EF.Application;

namespace TechZone.EF.Repositories
{
    internal class LaptopRepository : BaseRepository<Laptop>, ILaptopRepository
    {
        private readonly ApplicationDbContext _context;

        public LaptopRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Laptop> entities)
        {
            await _context.Laptops.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Delete(Laptop entity)
        {
            _context.Laptops.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<Laptop> entities)
        {
            _context.Laptops.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<Laptop> FirstOrDefaultAsync(Expression<Func<Laptop, bool>> criteria, params Expression<Func<Laptop, object>>[] includes)
        {
            IQueryable<Laptop> query = _context.Laptops;

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<Laptop>> GetAllAsync(params Expression<Func<Laptop, object>>[] includes)
        {
            IQueryable<Laptop> query = _context.Laptops;

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<Laptop> GetByIdAsync(int id, params Expression<Func<Laptop, object>>[] includes)
        {
            IQueryable<Laptop> query = _context.Laptops;

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Laptop>> WhereAsync(
            Expression<Func<Laptop, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<Laptop, object>>? orderBy = null,
            OrderBy orderDirection = OrderBy.Ascending,
            params Expression<Func<Laptop, object>>[] includes)
        {
            IQueryable<Laptop> query = _context.Laptops;

            if (includes != null)
            {
                foreach (var include in includes)
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
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }
    }
}
