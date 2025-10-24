using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptops.GetAllLaptops.Dtos;
using TechZoneV1.Features.Laptops.GetAllLaptops.Queries;
using TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Handlers
{
    public class GetAllLaptopsQueryHandler : IRequestHandler<GetAllLaptopsQuery, RequestResponse<PagedResult<LaptopResponseViewModel>>>
    {
        private readonly IBaseRepository<Laptop> _laptopRepository;

        public GetAllLaptopsQueryHandler(IBaseRepository<Laptop> laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }

        public async Task<RequestResponse<PagedResult<LaptopResponseViewModel>>> Handle(
            GetAllLaptopsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var queryDto = request.QueryDto;

                // Base query with includes
                var baseQuery = _laptopRepository.GetAll()
                    .Include(l => l.Brand)
                    .Include(l => l.Category)
                    .Include(l => l.Variants.Where(v => v.IsActive))
                    .Include(l => l.Images.Where(i => i.IsMain))
                    .Include(l => l.Ratings)
                    .Where(l => !l.IsDeleted);

                // Apply filters
                if (!string.IsNullOrEmpty(queryDto.Search))
                {
                    baseQuery = baseQuery.Where(l =>
                        l.ModelName.Contains(queryDto.Search) ||
                        l.Brand.Name.Contains(queryDto.Search) ||
                        l.Processor.Contains(queryDto.Search));
                }

                if (queryDto.BrandId.HasValue)
                {
                    baseQuery = baseQuery.Where(l => l.BrandId == queryDto.BrandId.Value);
                }

                if (queryDto.CategoryId.HasValue)
                {
                    baseQuery = baseQuery.Where(l => l.CategoryId == queryDto.CategoryId.Value);
                }

                if (queryDto.IsActive.HasValue)
                {
                    baseQuery = baseQuery.Where(l => l.IsActive == queryDto.IsActive.Value);
                }

                if (queryDto.ReleaseYear.HasValue)
                {
                    baseQuery = baseQuery.Where(l => l.ReleaseYear == queryDto.ReleaseYear.Value);
                }

                if (queryDto.HasCamera.HasValue)
                {
                    baseQuery = baseQuery.Where(l => l.HasCamera == queryDto.HasCamera.Value);
                }

                if (queryDto.HasTouchScreen.HasValue)
                {
                    baseQuery = baseQuery.Where(l => l.HasTouchScreen == queryDto.HasTouchScreen.Value);
                }

                // Apply sorting
                baseQuery = queryDto.SortBy switch
                {
                    FullLaptopSortBy.Price => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(l => l.Variants.Where(v => v.IsActive).Min(v => v.CurrentPrice))
                        : baseQuery.OrderByDescending(l => l.Variants.Where(v => v.IsActive).Max(v => v.CurrentPrice)),
                    FullLaptopSortBy.ReleaseYear => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(l => l.ReleaseYear)
                        : baseQuery.OrderByDescending(l => l.ReleaseYear),
                    FullLaptopSortBy.CreatedAt => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(l => l.CreatedAt)
                        : baseQuery.OrderByDescending(l => l.CreatedAt),
                    FullLaptopSortBy.AverageRating => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(l => l.Ratings.Average(r => r.Stars))
                        : baseQuery.OrderByDescending(l => l.Ratings.Average(r => r.Stars)),
                    FullLaptopSortBy.ModelName or _ => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(l => l.ModelName)
                        : baseQuery.OrderByDescending(l => l.ModelName)
                };

                // Get total count for pagination
                var totalCount = await baseQuery.CountAsync(cancellationToken);

                // Apply pagination and get data
                var laptops = await baseQuery
                    .Skip((queryDto.Page - 1) * queryDto.PageSize)
                    .Take(queryDto.PageSize)
                    .Select(l => new LaptopResponseViewModel
                    {
                        Id = l.Id,
                        ModelName = l.ModelName,
                        Brand = new BrandViewModel
                        {
                            Id = l.Brand.Id,
                            Name = l.Brand.Name,
                            LogoUrl = l.Brand.LogoUrl
                        },
                        Category = new CategoryViewModel
                        {
                            Id = l.Category.Id,
                            Name = l.Category.Name
                        },
                        Processor = l.Processor,
                        GPU = l.GPU ?? string.Empty,
                        ScreenSize = l.ScreenSize ?? string.Empty,
                        HasCamera = l.HasCamera,
                        HasKeyboard = l.HasKeyboard,
                        HasTouchScreen = l.HasTouchScreen,
                        ReleaseYear = l.ReleaseYear,
                        IsActive = l.IsActive,
                        VariantCount = l.Variants.Count(v => v.IsActive),
                        PriceRange = new PriceRangeViewModel
                        {
                            Min = l.Variants.Where(v => v.IsActive).Min(v => v.CurrentPrice),
                            Max = l.Variants.Where(v => v.IsActive).Max(v => v.CurrentPrice)
                        },
                        AverageRating = l.Ratings.Any() ? l.Ratings.Average(r => r.Stars) : 0,
                        MainImage = l.Images.FirstOrDefault(i => i.IsMain) != null ?
                                   l.Images.First(i => i.IsMain).ImageUrl : string.Empty
                    })
                    .ToListAsync(cancellationToken);

                if (!laptops.Any())
                {
                    return RequestResponse<PagedResult<LaptopResponseViewModel>>.Fail(
                        "No laptops found",
                        "لم يتم العثور على أجهزة لابتوب"
                    );
                }

                var pagedResult = new PagedResult<LaptopResponseViewModel>(
                    laptops,
                    totalCount,
                    queryDto.Page,
                    queryDto.PageSize
                );

                return RequestResponse<PagedResult<LaptopResponseViewModel>>.Success(
                    pagedResult,
                    "Laptops fetched successfully",
                    "تم جلب أجهزة اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<PagedResult<LaptopResponseViewModel>>.Fail(
                    "An error occurred while fetching laptops",
                    "حدث خطأ أثناء جلب أجهزة اللابتوب"
                );
            }
        }
    }
}