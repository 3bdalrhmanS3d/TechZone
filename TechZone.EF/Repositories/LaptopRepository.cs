using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechZone.Core.Consts;
using TechZone.Core.Entities;
using TechZone.Core.ENUMS.Laptop;
using TechZone.Core.Interfaces;
using TechZone.Core.PagedResult;
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

        public async Task<PagedResult<Laptop>> GetPagedAsync(PaginationParamsDto<LaptopSortBy> paginationParams)
        {
            var query = _context.Laptops
                .Include(l => l.Variants)
                .AsNoTracking()
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var searchTerm = paginationParams.Search.ToLower();
                query = query.Where(l =>
                    l.ModelName.ToLower().Contains(searchTerm) ||
                    l.Processor.ToLower().Contains(searchTerm) ||
                    l.GPU.ToLower().Contains(searchTerm) ||
                    l.ScreenSize.ToLower().Contains(searchTerm) ||
                    l.Ports.ToLower().Contains(searchTerm)
                );
            }

            // Apply sorting
            query = ApplySorting(query, paginationParams.SortBy, paginationParams.SortDirection);

            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedResult<Laptop>(items, totalCount, paginationParams.Page, paginationParams.PageSize);
        }

        private IQueryable<Laptop> ApplySorting(IQueryable<Laptop> query, LaptopSortBy? sortBy, SortDirection sortDirection)
        {
            if (!sortBy.HasValue)
                return query.OrderBy(l => l.Id); // Default sorting

            return sortBy.Value switch
            {
                LaptopSortBy.Id => sortDirection == SortDirection.Asc
                    ? query.OrderBy(l => l.Id)
                    : query.OrderByDescending(l => l.Id),

                

                _ => query.OrderBy(l => l.Id)
            };
        }

        public async Task<int> CountAsync()
        {
            return await _context.Laptops.CountAsync();
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
