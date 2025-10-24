using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.LaptopVariant.GetVariantById.Queries;
using TechZoneV1.Features.LaptopVariant.GetVariantById.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantById.Handlers
{
    public class GetLaptopVariantByIdQueryHandler : IRequestHandler<GetLaptopVariantByIdQuery, RequestResponse<LaptopVariantDetailsViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<ProductDiscount> _productDiscountRepository;
        private readonly IBaseRepository<Discount> _discountRepository;

        public GetLaptopVariantByIdQueryHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<ProductDiscount> productDiscountRepository,
            IBaseRepository<Discount> discountRepository)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _productDiscountRepository = productDiscountRepository;
            _discountRepository = discountRepository;
        }

        public async Task<RequestResponse<LaptopVariantDetailsViewModel>> Handle(
            GetLaptopVariantByIdQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var currentDate = DateTime.UtcNow;

                // Get the laptop variant with all includes
                var laptopVariant = await _laptopVariantRepository.GetAll()
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Brand)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Category)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Images)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Ports)
                    .Include(lv => lv.Laptop)
                        .ThenInclude(l => l.Warranties)
                    .Include(lv => lv.PriceHistories)
                    .Include(lv => lv.ProductDiscounts)
                    .FirstOrDefaultAsync(lv => lv.Id == request.Id && lv.IsActive, cancellationToken);

                if (laptopVariant == null)
                {
                    return RequestResponse<LaptopVariantDetailsViewModel>.Fail(
                        "Laptop variant not found",
                        "نوع اللابتوب غير موجود"
                    );
                }

                // Get active discounts for this variant
                var activeDiscounts = await _discountRepository.GetAll()
                    .Where(d => d.IsActive &&
                                d.StartDate <= currentDate &&
                                d.EndDate >= currentDate)
                    .ToListAsync(cancellationToken);

                var productDiscounts = await _productDiscountRepository.GetAll()
                    .Where(pd => pd.ProductType == ProductType.LaptopVariant)
                    .ToListAsync(cancellationToken);

                // Find active discount for this laptop variant
                var productDiscount = productDiscounts
                    .FirstOrDefault(pd => pd.ProductId == laptopVariant.Id);

                Discount? activeDiscount = null;
                if (productDiscount != null)
                {
                    activeDiscount = activeDiscounts
                        .FirstOrDefault(d => d.Id == productDiscount.DiscountId);
                }

                var (originalPrice, discountPercentage, discountAmount) =
                    CalculatePricing(laptopVariant, activeDiscount);

                // Map to ViewModel
                var variantDetails = new LaptopVariantDetailsViewModel
                {
                    Id = laptopVariant.Id,
                    Sku = laptopVariant.SKU,
                    Ram = laptopVariant.RAM,
                    Storage = laptopVariant.StorageCapacityGB,
                    StorageType = laptopVariant.StorageType,
                    CurrentPrice = laptopVariant.CurrentPrice,
                    StockQuantity = laptopVariant.StockQuantity,
                    ReservedQuantity = laptopVariant.ReservedQuantity,
                    AvailableQuantity = laptopVariant.StockQuantity - laptopVariant.ReservedQuantity,
                    ReorderLevel = laptopVariant.ReorderLevel,
                    IsActive = laptopVariant.IsActive,
                    CreatedAt = laptopVariant.CreatedAt,
                    UpdatedAt = laptopVariant.UpdatedAt ?? laptopVariant.CreatedAt,
                    StockStatus = GetStockStatus(laptopVariant),
                    OriginalPrice = originalPrice,
                    DiscountPercentage = discountPercentage,
                    DiscountAmount = discountAmount,
                    Laptop = new LaptopDetailsViewModel
                    {
                        Id = laptopVariant.Laptop.Id,
                        ModelName = laptopVariant.Laptop.ModelName,
                        Processor = laptopVariant.Laptop.Processor,
                        Gpu = laptopVariant.Laptop.GPU,
                        ScreenSize = laptopVariant.Laptop.ScreenSize,
                        HasCamera = laptopVariant.Laptop.HasCamera,
                        HasKeyboard = laptopVariant.Laptop.HasKeyboard,
                        HasTouchScreen = laptopVariant.Laptop.HasTouchScreen,
                        Description = laptopVariant.Laptop.Description,
                        ReleaseYear = laptopVariant.Laptop.ReleaseYear,
                        StoreLocation = laptopVariant.Laptop.StoreLocation,
                        StoreContact = laptopVariant.Laptop.StoreContact
                    },
                    Brand = new BrandDetailsViewModel
                    {
                        Id = laptopVariant.Laptop.Brand.Id,
                        Name = laptopVariant.Laptop.Brand.Name,
                        Country = laptopVariant.Laptop.Brand.Country,
                        LogoUrl = laptopVariant.Laptop.Brand.LogoUrl
                    },
                    Category = new CategoryDetailsViewModel
                    {
                        Id = laptopVariant.Laptop.Category.Id,
                        Name = laptopVariant.Laptop.Category.Name,
                        Description = laptopVariant.Laptop.Category.Description
                    },
                    Ports = laptopVariant.Laptop.Ports.Select(p => new PortViewModel
                    {
                        Id = p.Id,
                        Type = p.PortType,
                        Quantity = p.Quantity
                    }).ToList(),
                    Warranty = laptopVariant.Laptop.Warranties.Select(w => new WarrantyViewModel
                    {
                        Id = w.Id,
                        DurationMonths = w.DurationMonths,
                        Type = w.Type,
                        Coverage = w.Coverage,
                        Provider = w.Provider
                    }).FirstOrDefault(), // Take the first warranty
                    Images = laptopVariant.Laptop.Images.Select(i => new ImageViewModel
                    {
                        Id = i.Id,
                        Url = i.ImageUrl,
                        IsMain = i.IsMain,
                        DisplayOrder = i.DisplayOrder
                    }).OrderBy(i => i.DisplayOrder).ToList()
                };

                return RequestResponse<LaptopVariantDetailsViewModel>.Success(
                    variantDetails,
                    "Laptop variant details fetched successfully",
                    "تم جلب تفاصيل نوع اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<LaptopVariantDetailsViewModel>.Fail(
                    "An error occurred while fetching laptop variant details",
                    "حدث خطأ أثناء جلب تفاصيل نوع اللابتوب"
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