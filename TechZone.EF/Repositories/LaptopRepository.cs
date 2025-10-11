using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechZone.Core.Consts;
using TechZone.Core.DTOs.Laptop;
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
        public async Task<IEnumerable<FullLaptopResponseDTO>> GetAllFullAsync()
        {
            var laptops = await _context.Laptops
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Images)
                .Include(l => l.Variants)
                .Include(l => l.Ports)
                .Include(l => l.Warranties)
                .AsNoTracking()
                .ToListAsync();

            return laptops.Select(l => new FullLaptopResponseDTO
            {
                Id = l.Id,
                ModelName = l.ModelName,
                Processor = l.Processor,
                GPU = l.GPU,
                ScreenSize = l.ScreenSize,
                HasCamera = l.HasCamera,
                HasKeyboard = l.HasKeyboard,
                HasTouchScreen = l.HasTouchScreen,
                Ports = l.Ports,
                Description = l.Description,
                Warranty = l.Warranties,

                Brand = new BrandDTO
                {
                    Id = l.Brand.Id,
                    Name = l.Brand.Name
                },
                Category = new CategoryDTO
                {
                    Id = l.Category.Id,
                    Name = l.Category.Name
                },
                Variants = l.Variants.Select(v => new LaptopVariantDTO
                {
                    Id = v.Id,
                    Ram = v.RAM,
                    Storage = v.StorageCapacityGB,
                    Price = v.CurrentPrice,
                    StockQuantity = v.StockQuantity
                }).ToList(),
                Images = l.Images.Select(i => new LaptopImageDTO
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    IsMain = i.IsMain
                }).ToList()
            }).ToList();
        }


        public async Task<PagedResult<LaptopResponseDTO>> GetPagedAsync(PaginationParamsDto<LaptopSortBy> paginationParams)
        {
            // FIXED: Remove the invalid cast by storing the query in a separate variable
            IQueryable<Laptop> baseQuery = _context.Laptops
                .AsNoTracking()
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Images)
                .Include(l => l.Variants);

            // Filter (null-safe)
            if (!string.IsNullOrWhiteSpace(paginationParams.Search))
            {
                var s = paginationParams.Search.ToLower();
                baseQuery = baseQuery.Where(l =>
                    ((l.ModelName ?? "").ToLower().Contains(s)) ||
                    ((l.Processor ?? "").ToLower().Contains(s)) ||
                    ((l.GPU ?? "").ToLower().Contains(s)) ||
                    ((l.ScreenSize ?? "").ToLower().Contains(s)) ||
                    (l.Ports.Any(p => (p.PortType ?? "").ToLower().Contains(s)))
                );
            }

            // ✅ ALWAYS order before paging
            var orderedQuery = ApplySorting(baseQuery, paginationParams.SortBy, paginationParams.SortDirection);

            // Projection with actual data from new schema
            var projected = orderedQuery.Select(l => new
            {
                l.Id,
                Name = l.ModelName ?? "",
                Category = l.Category != null ? l.Category.Name : "unknown",
                Brand = l.Brand != null ? l.Brand.Name : "",
                Processor = l.Processor ?? "",
                GPU = l.GPU ?? "",
                ScreenSize = l.ScreenSize ?? "",

                // Use actual CurrentPrice from variants instead of hardcoded value
                MinPrice = l.Variants.Any() ? l.Variants.Min(v => v.CurrentPrice) : 0m,
                MaxPrice = l.Variants.Any() ? l.Variants.Max(v => v.CurrentPrice) : 0m,

                Images = l.Images.Select(img => img.ImageUrl ?? "").ToList(),
                Variants = l.Variants.Select(v => new
                {
                    v.RAM,
                    v.StorageCapacityGB,
                    v.StorageType,
                    v.CurrentPrice
                }).ToList(),

                // Build description from actual data
                ShortDescription = string.Concat(
                    (l.Brand != null ? l.Brand.Name : ""),
                    ", ",
                    (l.Processor ?? ""),
                    ", ",
                    l.Variants.Any() ? $"Up to {l.Variants.Max(v => v.RAM)}GB RAM" : "RAM info not available"
                )
            });

            var totalCount = await projected.CountAsync();

            // Paging
            var page = paginationParams.Page;
            var size = paginationParams.PageSize;

            var pageItems = await projected
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            // Post-processing
            var rnd = new Random();
            var items = pageItems.Select(x => new LaptopResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.MinPrice, // Use actual min price
                Category = x.Category,
                Images = x.Images.Where(u => !string.IsNullOrWhiteSpace(u)).ToList(),
                ReviewsCount = rnd.Next(0, 500), // Keep random for demo
                IsDiscounted = false, // You can implement discount logic later
                DiscountedPrice = null,
                Rate = Math.Round(rnd.NextDouble() * 5, 1), // Keep random for demo
                ShortDescription = x.ShortDescription,
                Brand = x.Brand,
                Processor = x.Processor,
                GPU = x.GPU,
                ScreenSize = x.ScreenSize,
                PriceRange = x.MinPrice == x.MaxPrice ?
                    $"{x.MinPrice:C}" :
                    $"{x.MinPrice:C} - {x.MaxPrice:C}"
            }).ToList();

            return new PagedResult<LaptopResponseDTO>(items, totalCount, page, size);
        }


        private static IQueryable<Laptop> ApplySorting(
            IQueryable<Laptop> q,
            LaptopSortBy? sortBy,
            SortDirection dir)
        {
            IOrderedQueryable<Laptop> oq;

            switch (sortBy)
            {
                case LaptopSortBy.Price:
                    oq = (dir == SortDirection.Asc)
                        ? q.OrderBy(l => l.Variants
                                .Where(v => v.IsActive)
                                .Select(v => (decimal?)v.CurrentPrice) // Updated from Price to CurrentPrice
                                .DefaultIfEmpty()
                                .Min())
                        : q.OrderByDescending(l => l.Variants
                                .Where(v => v.IsActive)
                                .Select(v => (decimal?)v.CurrentPrice) // Updated from Price to CurrentPrice
                                .DefaultIfEmpty()
                                .Min());
                    break;

                case LaptopSortBy.ModelName:
                    oq = (dir == SortDirection.Asc)
                        ? q.OrderBy(l => l.ModelName)
                        : q.OrderByDescending(l => l.ModelName);
                    break;

                case LaptopSortBy.Brand:
                    oq = (dir == SortDirection.Asc)
                        ? q.OrderBy(l => l.Brand.Name)
                        : q.OrderByDescending(l => l.Brand.Name);
                    break;

                default:
                    // Default sorting by ID
                    oq = (dir == SortDirection.Asc)
                        ? q.OrderBy(l => l.Id)
                        : q.OrderByDescending(l => l.Id);
                    break;
            }

            // Additional sorting for consistent results
            return (dir == SortDirection.Asc)
                ? oq.ThenBy(l => l.Id)
                : oq.ThenByDescending(l => l.Id);
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

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(criteria);
        }

        public async Task<IEnumerable<Laptop>> GetAllAsync(params Expression<Func<Laptop, object>>[] includes)
        {
            IQueryable<Laptop> query = _context.Laptops;

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }


        public async Task<Laptop> GetByIdAsync(int id, params Expression<Func<Laptop, object>>[] includes)
        {
            IQueryable<Laptop> query = _context.Laptops;

            if (includes != null && includes.Length > 0)
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

            if (includes != null && includes.Length > 0)
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

        // New method to get laptops with all related data
        public async Task<Laptop> GetLaptopWithDetailsAsync(int id)
        {
            return await _context.Laptops
                .Include(l => l.Brand)
                .Include(l => l.Category)
                .Include(l => l.Images)
                .Include(l => l.Variants)
                .Include(l => l.Ports)
                .Include(l => l.Warranties)
                .Include(l => l.Ratings)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        // New method to get featured laptops
        public async Task<IEnumerable<Laptop>> GetFeaturedLaptopsAsync(int count = 10)
        {
            return await _context.Laptops
                .Where(l => l.IsActive)
                .Include(l => l.Brand)
                .Include(l => l.Images)
                .Include(l => l.Variants)
                .OrderByDescending(l => l.Variants.Any() ? l.Variants.Max(v => v.CurrentPrice) : 0)
                .Take(count)
                .ToListAsync();
        }
    }
}