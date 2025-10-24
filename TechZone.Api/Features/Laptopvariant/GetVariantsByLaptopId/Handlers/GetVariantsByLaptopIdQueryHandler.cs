using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Queries;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Handlers
{
    public class GetVariantsByLaptopIdQueryHandler : IRequestHandler<GetVariantsByLaptopIdQuery, RequestResponse<VariantsByLaptopIdResponseViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IBaseRepository<ProductDiscount> _productDiscountRepository;
        private readonly IBaseRepository<Discount> _discountRepository;

        public GetVariantsByLaptopIdQueryHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<Laptop> laptopRepository,
            IBaseRepository<ProductDiscount> productDiscountRepository,
            IBaseRepository<Discount> discountRepository)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _laptopRepository = laptopRepository;
            _productDiscountRepository = productDiscountRepository;
            _discountRepository = discountRepository;
        }

        public async Task<RequestResponse<VariantsByLaptopIdResponseViewModel>> Handle(
            GetVariantsByLaptopIdQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var queryDto = request.QueryDto;
                var currentDate = DateTime.UtcNow;

                // First, check if the laptop exists and get its basic info
                var laptop = await _laptopRepository.GetAll()
                    .Where(l => l.Id == request.LaptopId && l.IsActive)
                    .Select(l => new LaptopSummaryViewModel
                    {
                        Id = l.Id,
                        ModelName = l.ModelName,
                        Processor = l.Processor,
                        Gpu = l.GPU,
                        ScreenSize = l.ScreenSize,
                        HasCamera = l.HasCamera,
                        HasTouchScreen = l.HasTouchScreen
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (laptop == null)
                {
                    return RequestResponse<VariantsByLaptopIdResponseViewModel>.Fail(
                        "Laptop not found",
                        "اللابتوب غير موجود"
                    );
                }

                // Get all active discounts for laptop variants
                var activeDiscounts = await _discountRepository.GetAll()
                    .Where(d => d.IsActive &&
                                d.StartDate <= currentDate &&
                                d.EndDate >= currentDate)
                    .ToListAsync(cancellationToken);

                var productDiscounts = await _productDiscountRepository.GetAll()
                    .Where(pd => pd.ProductType == ProductType.LaptopVariant)
                    .ToListAsync(cancellationToken);

                // Base query for variants of this laptop
                var baseQuery = _laptopVariantRepository.GetAll()
                    .Include(lv => lv.PriceHistories)
                    .Include(lv => lv.ProductDiscounts)
                    .Where(lv => lv.LaptopId == request.LaptopId && lv.IsActive);

                // Apply in-stock filter
                if (queryDto.InStockOnly)
                {
                    baseQuery = baseQuery.Where(lv => (lv.StockQuantity - lv.ReservedQuantity) > 0);
                }

                // Get total count for pagination
                var totalCount = await baseQuery.CountAsync(cancellationToken);

                // Apply pagination and get the data
                var laptopVariants = await baseQuery
                    .Skip((queryDto.Page - 1) * queryDto.PageSize)
                    .Take(queryDto.PageSize)
                    .ToListAsync(cancellationToken);

                if (!laptopVariants.Any())
                {
                    return RequestResponse<VariantsByLaptopIdResponseViewModel>.Fail(
                        "No variants found for this laptop",
                        "لم يتم العثور على أنواع لهذا اللابتوب"
                    );
                }

                // Map to ViewModel with discount calculations
                var variantItems = laptopVariants.Select(lv =>
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

                    var (originalPrice, discountPercentage, _) = CalculatePricing(lv, activeDiscount);

                    return new LaptopVariantSummaryViewModel
                    {
                        Id = lv.Id,
                        Sku = lv.SKU,
                        Ram = lv.RAM,
                        Storage = lv.StorageCapacityGB,
                        StorageType = lv.StorageType,
                        CurrentPrice = lv.CurrentPrice,
                        OriginalPrice = originalPrice,
                        DiscountPercentage = discountPercentage,
                        StockQuantity = lv.StockQuantity,
                        ReservedQuantity = lv.ReservedQuantity,
                        AvailableQuantity = lv.StockQuantity - lv.ReservedQuantity,
                        StockStatus = GetStockStatus(lv),
                        IsActive = lv.IsActive
                    };
                }).ToList();

                // Create paged result
                var pagedVariants = new PagedResult<LaptopVariantSummaryViewModel>(
                    variantItems,
                    totalCount,
                    queryDto.Page,
                    queryDto.PageSize
                );

                var response = new VariantsByLaptopIdResponseViewModel
                {
                    Laptop = laptop,
                    Variants = pagedVariants
                };

                return RequestResponse<VariantsByLaptopIdResponseViewModel>.Success(
                    response,
                    "Laptop variants fetched successfully",
                    "تم جلب أنواع اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<VariantsByLaptopIdResponseViewModel>.Fail(
                    "An error occurred while fetching laptop variants",
                    "حدث خطأ أثناء جلب أنواع اللابتوب"
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