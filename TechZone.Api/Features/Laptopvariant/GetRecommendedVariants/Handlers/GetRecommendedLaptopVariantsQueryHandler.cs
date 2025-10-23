using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Queries;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Handlers
{
    public class GetRecommendedLaptopVariantsQueryHandler : IRequestHandler<GetRecommendedLaptopVariantsQuery, RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>>
    {
        private readonly IBaseRepository<LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<ProductDiscount> _productDiscountRepository;
        private readonly IBaseRepository<Discount> _discountRepository;

        public GetRecommendedLaptopVariantsQueryHandler(
            IBaseRepository<LaptopVariant> laptopVariantRepository,
            IBaseRepository<ProductDiscount> productDiscountRepository,
            IBaseRepository<Discount> discountRepository)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _productDiscountRepository = productDiscountRepository;
            _discountRepository = discountRepository;
        }

        public async Task<RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>> Handle(
            GetRecommendedLaptopVariantsQuery request,
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

                if (queryDto.CategoryId.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.Laptop.CategoryId == queryDto.CategoryId.Value);
                }

                if (queryDto.BrandId.HasValue)
                {
                    baseQuery = baseQuery.Where(lv => lv.Laptop.BrandId == queryDto.BrandId.Value);
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
                        "No recommended laptop variants found",
                        "لم يتم العثور على أنواع اللابتوب الموصى بها"
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

                    return new RecommendedLaptopVariantViewModel
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
                }).ToList();

                var pagedResult = new PagedResult<RecommendedLaptopVariantViewModel>(
                    items,
                    totalCount,
                    queryDto.Page,
                    queryDto.PageSize
                );

                return RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>.Success(
                    pagedResult,
                    "Recommended laptop variants fetched successfully",
                    "تم جلب أنواع اللابتوب الموصى بها بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>.Fail(
                    "An error occurred while fetching recommended laptop variants",
                    "حدث خطأ أثناء جلب أنواع اللابتوب الموصى بها"
                );
            }
        }

        private static string GetStockStatus(LaptopVariant variant)
        {
            var availableQuantity = variant.StockQuantity - variant.ReservedQuantity;

            if (availableQuantity <= 0) return "OutOfStock";
            if (availableQuantity <= variant.ReorderLevel) return "LowStock";
            return "InStock";
        }

        private static (decimal originalPrice, decimal discountPercentage, decimal discountAmount)
            CalculatePricing(LaptopVariant variant, Discount? activeDiscount)
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
                    // For percentage discount, calculate what the original price would be
                    // currentPrice = originalPrice * (1 - discountPercentage/100)
                    // So: originalPrice = currentPrice / (1 - discountPercentage/100)
                    var discountRate = activeDiscount.Value / 100m;
                    if (discountRate < 1) // Avoid division by zero and invalid rates
                    {
                        originalPrice = variant.CurrentPrice / (1 - discountRate);
                        discountAmount = originalPrice - variant.CurrentPrice;
                        discountPercentage = activeDiscount.Value;
                    }
                }
                else // FixedAmount
                {
                    // For fixed amount discount: originalPrice = currentPrice + discountAmount
                    discountAmount = activeDiscount.Value;
                    originalPrice = variant.CurrentPrice + discountAmount;
                    if (originalPrice > 0)
                    {
                        discountPercentage = Math.Round((discountAmount / originalPrice) * 100, 2);
                    }
                }

                // Apply max discount amount constraint if applicable
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
                // No active discount, check price history for recent price changes
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