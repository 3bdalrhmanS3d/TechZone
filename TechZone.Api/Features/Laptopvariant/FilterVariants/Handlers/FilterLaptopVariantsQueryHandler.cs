using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels;
using TechZoneV1.Features.LaptopVariant.FilterVariants.Queries;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.FilterVariants.Handlers
{
    public class FilterLaptopVariantsQueryHandler : IRequestHandler<FilterLaptopVariantsQuery, RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<ProductDiscount> _productDiscountRepository;
        private readonly IBaseRepository<Discount> _discountRepository;

        public FilterLaptopVariantsQueryHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<ProductDiscount> productDiscountRepository,
            IBaseRepository<Discount> discountRepository)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _productDiscountRepository = productDiscountRepository;
            _discountRepository = discountRepository;
        }

        public async Task<RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>> Handle(
            FilterLaptopVariantsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var queryDto = request.QueryDto;
                var currentDate = DateTime.UtcNow;

                // Get all active discounts for laptop variants
                var activeDiscounts = await _discountRepository.GetAll()
                    .Where(d => d.IsActive &&
                                d.StartDate <= currentDate &&
                                d.EndDate >= currentDate)
                    .ToListAsync(cancellationToken);

                // Get product discounts for laptop variants
                var productDiscounts = await _productDiscountRepository.GetAll()
                    .Where(pd => pd.ProductType == ProductType.LaptopVariant)
                    .ToListAsync(cancellationToken);

                // Base query with includes
                var baseQuery = _laptopVariantRepository.GetAll()
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Brand)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Category)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Images)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Ratings)
                    .Include(lv => lv.PriceHistories)
                    .Where(lv => lv.IsActive && lv.Laptop.IsActive);

                // Apply filters
                if (!string.IsNullOrEmpty(queryDto.Search))
                {
                    baseQuery = baseQuery.Where(lv =>
                        lv.Laptop.ModelName.Contains(queryDto.Search) ||
                        lv.SKU.Contains(queryDto.Search) ||
                        lv.Laptop.Brand.Name.Contains(queryDto.Search));
                }

                // Brand filter
                if (queryDto.BrandIds != null && queryDto.BrandIds.Any())
                {
                    baseQuery = baseQuery.Where(lv => queryDto.BrandIds.Contains(lv.Laptop.BrandId));
                }

                // Category filter
                if (queryDto.CategoryIds != null && queryDto.CategoryIds.Any())
                {
                    baseQuery = baseQuery.Where(lv => queryDto.CategoryIds.Contains(lv.Laptop.CategoryId));
                }

                // Price range filter
                if (queryDto.MinPrice.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.CurrentPrice >= queryDto.MinPrice.Value);
                }

                if (queryDto.MaxPrice.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.CurrentPrice <= queryDto.MaxPrice.Value);
                }

                // Processor filter
                if (!string.IsNullOrEmpty(queryDto.Processor))
                {
                    baseQuery = baseQuery.Where(lv => lv.Laptop.Processor.Contains(queryDto.Processor));
                }

                // RAM filter
                if (queryDto.Ram != null && queryDto.Ram.Any())
                {
                    baseQuery = baseQuery.Where(lv => queryDto.Ram.Contains(lv.RAM));
                }

                // Storage filter
                if (queryDto.Storage != null && queryDto.Storage.Any())
                {
                    baseQuery = baseQuery.Where(lv => queryDto.Storage.Contains(lv.StorageCapacityGB));
                }

                // Storage Type filter
                if (!string.IsNullOrEmpty(queryDto.StorageType))
                {
                    baseQuery = baseQuery.Where(lv => lv.StorageType == queryDto.StorageType);
                }

                // Screen Size filter
                if (queryDto.ScreenSize != null && queryDto.ScreenSize.Any())
                {
                    baseQuery = baseQuery.Where(lv => queryDto.ScreenSize.Contains(lv.Laptop.ScreenSize));
                }

                // Camera filter
                if (queryDto.HasCamera.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.Laptop.HasCamera == queryDto.HasCamera.Value);
                }

                // Touch Screen filter
                if (queryDto.HasTouchScreen.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.Laptop.HasTouchScreen == queryDto.HasTouchScreen.Value);
                }

                // In Stock filter
                if (queryDto.InStock.HasValue && queryDto.InStock.Value)
                {
                    baseQuery = baseQuery.Where(lv => (lv.StockQuantity - lv.ReservedQuantity) > 0);
                }

                // Release Year filter
                if (queryDto.ReleaseYear.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.Laptop.ReleaseYear == queryDto.ReleaseYear.Value);
                }

                // Apply sorting
                baseQuery = queryDto.SortBy switch
                {
                    RecommendedLaptopVariantSortBy.Price => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(lv => lv.CurrentPrice)
                        : baseQuery.OrderByDescending(lv => lv.CurrentPrice),
                    RecommendedLaptopVariantSortBy.Name => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(lv => lv.Laptop.ModelName)
                        : baseQuery.OrderByDescending(lv => lv.Laptop.ModelName),
                    RecommendedLaptopVariantSortBy.Rating => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(lv => lv.Laptop.Ratings.Average(r => r.Stars))
                        : baseQuery.OrderByDescending(lv => lv.Laptop.Ratings.Average(r => r.Stars)),
                    RecommendedLaptopVariantSortBy.CreatedAt or _ => queryDto.SortDirection == SortDirection.Asc
                        ? baseQuery.OrderBy(lv => lv.CreatedAt)
                        : baseQuery.OrderByDescending(lv => lv.CreatedAt)
                };

                // Get total count for pagination
                var totalCount = await baseQuery.CountAsync(cancellationToken);

                // Apply pagination and get the data
                var laptopVariants = await baseQuery
                    .Skip((queryDto.Page - 1) * queryDto.PageSize)
                    .Take(queryDto.PageSize)
                    .ToListAsync(cancellationToken);

                if (!laptopVariants.Any())
                {
                    return RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>.Fail(
                        "No laptop variants found matching the criteria",
                        "لم يتم العثور على أنواع اللابتوب التي تطابق المعايير"
                    );
                }

                // Map to ViewModel with discount calculations
                var items = laptopVariants.Select(lv =>
                {
                    // Find active discount for this laptop variant
                    var productDiscount = productDiscounts
                        .FirstOrDefault(pd => pd.ProductId == lv.Id);

                    Discount? activeDiscount = null;
                    if (productDiscount != null)
                    {
                        activeDiscount = activeDiscounts
                            .FirstOrDefault(d => d.Id == productDiscount.DiscountId);
                    }

                    var (originalPrice, discountPercentage, discountAmount) =
                        CalculatePricing(lv, activeDiscount);

                    var viewModel = new RecommendedLaptopVariantViewModel
                    {
                        Id = lv.Id,
                        Sku = lv.SKU,
                        Ram = lv.RAM,
                        Storage = lv.StorageCapacityGB,
                        StorageType = lv.StorageType,
                        CurrentPrice = lv.CurrentPrice,
                        StockQuantity = lv.StockQuantity,
                        ReservedQuantity = lv.ReservedQuantity,
                        AvailableQuantity = lv.StockQuantity - lv.ReservedQuantity,
                        ReorderLevel = lv.ReorderLevel,
                        IsActive = lv.IsActive,
                        StockStatus = GetStockStatus(lv),
                        Laptop = new LaptopViewModel
                        {
                            Id = lv.Laptop.Id,
                            ModelName = lv.Laptop.ModelName,
                            Processor = lv.Laptop.Processor,
                            Gpu = lv.Laptop.GPU,
                            ScreenSize = lv.Laptop.ScreenSize,
                            HasCamera = lv.Laptop.HasCamera,
                            HasTouchScreen = lv.Laptop.HasTouchScreen,
                            StoreLocation = lv.Laptop.StoreLocation
                        },
                        Brand = new BrandViewModel
                        {
                            Id = lv.Laptop.Brand.Id,
                            Name = lv.Laptop.Brand.Name,
                            LogoUrl = lv.Laptop.Brand.LogoUrl
                        },
                        Category = new CategoryViewModel
                        {
                            Id = lv.Laptop.Category.Id,
                            Name = lv.Laptop.Category.Name
                        },
                        Images = lv.Laptop.Images
                            .Where(img => img.IsMain)
                            .OrderBy(img => img.DisplayOrder)
                            .Select(img => img.ImageUrl)
                            .Take(2)
                            .ToList(),
                        AverageRating = lv.Laptop.Ratings.Any()
                            ? lv.Laptop.Ratings.Average(r => r.Stars)
                            : 0,
                        ReviewCount = lv.Laptop.Ratings.Count,
                        OriginalPrice = originalPrice,
                        DiscountPercentage = discountPercentage,
                        DiscountAmount = discountAmount
                    };

                    // Apply hasDiscount filter after mapping
                    if (queryDto.HasDiscount.HasValue && queryDto.HasDiscount.Value)
                    {
                        return discountPercentage > 0 ? viewModel : null;
                    }

                    return viewModel;

                }).Where(x => x != null).ToList();

                // Apply minRating filter after mapping (since it depends on calculated average rating)
                if (queryDto.MinRating.HasValue)
                {
                    items = items.Where(item => (decimal)item.AverageRating >= queryDto.MinRating.Value).ToList();
                }

                if (!items.Any())
                {
                    return RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>.Fail(
                        "No laptop variants found matching the criteria",
                        "لم يتم العثور على أنواع اللابتوب التي تطابق المعايير"
                    );
                }

                var pagedResult = new PagedResult<RecommendedLaptopVariantViewModel>(
                    items,
                    totalCount,
                    queryDto.Page,
                    queryDto.PageSize
                );

                return RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>.Success(
                    pagedResult,
                    "Laptop variants filtered successfully",
                    "تم تصفية أنواع اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>.Fail(
                    "An error occurred while filtering laptop variants",
                    "حدث خطأ أثناء تصفية أنواع اللابتوب"
                );
            }
        }

        private static string GetStockStatus(TechZone.Domain.Entities.LaptopVariant variant)
        {
            var availableQuantity = variant.StockQuantity - variant.ReservedQuantity;

            if (availableQuantity <= 0) return "OutOfStock";
            if (availableQuantity <= variant.ReorderLevel) return "LowStock";
            return "InStock";
        }

        private static (decimal originalPrice, decimal discountPercentage, decimal discountAmount)
            CalculatePricing(TechZone.Domain.Entities.LaptopVariant variant, Discount? activeDiscount)
        {
            decimal originalPrice = variant.CurrentPrice;
            decimal discountPercentage = 0m;
            decimal discountAmount = 0m;

            // Check for active discount
            if (activeDiscount != null)
            {
                // Calculate the original price based on discount type
                if (activeDiscount.DiscountType == DiscountType.Percentage)
                {
                    var discountRate = activeDiscount.Value / 100m;
                    if (discountRate < 1)
                    {
                        originalPrice = variant.CurrentPrice / (1 - discountRate);
                        discountAmount = originalPrice - variant.CurrentPrice;
                        discountPercentage = activeDiscount.Value;
                    }
                }
                else // FixedAmount
                {
                    discountAmount = activeDiscount.Value;
                    originalPrice = variant.CurrentPrice + discountAmount;
                    if (originalPrice > 0)
                    {
                        discountPercentage = Math.Round((discountAmount / originalPrice) * 100, 2);
                    }
                }

                // Apply max discount amount constraint
                if (activeDiscount.MaxDiscountAmount > 0 && discountAmount > activeDiscount.MaxDiscountAmount)
                {
                    discountAmount = activeDiscount.MaxDiscountAmount;
                    originalPrice = variant.CurrentPrice + discountAmount;
                    if (originalPrice > 0)
                    {
                        discountPercentage = Math.Round((discountAmount / originalPrice) * 100, 2);
                    }
                }
            }
            else
            {
                // Check price history for recent price changes
                var lastPriceChange = variant.PriceHistories
                    .OrderByDescending(ph => ph.EffectiveFrom)
                    .FirstOrDefault();

                if (lastPriceChange != null && lastPriceChange.OldPrice > variant.CurrentPrice)
                {
                    originalPrice = lastPriceChange.OldPrice;
                    discountAmount = originalPrice - variant.CurrentPrice;
                    discountPercentage = Math.Round((discountAmount / originalPrice) * 100, 2);
                }
                else
                {
                    originalPrice = variant.CurrentPrice;
                }
            }

            return (originalPrice, discountPercentage, discountAmount);
        }
    }
}