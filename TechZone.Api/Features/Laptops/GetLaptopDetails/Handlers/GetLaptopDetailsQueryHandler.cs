using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Laptops.GetLaptopDetails.Queries;
using TechZoneV1.Features.Laptops.GetLaptopDetails.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetLaptopDetails.Handlers
{
    public class GetLaptopDetailsQueryHandler : IRequestHandler<GetLaptopDetailsQuery, RequestResponse<LaptopDetailsResponseViewModel>>
    {
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IBaseRepository<OrderItem> _orderItemRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;

        public GetLaptopDetailsQueryHandler(
            IBaseRepository<Laptop> laptopRepository,
            IBaseRepository<OrderItem> orderItemRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository)
        {
            _laptopRepository = laptopRepository;
            _orderItemRepository = orderItemRepository;
            _laptopVariantRepository = laptopVariantRepository;
        }

        public async Task<RequestResponse<LaptopDetailsResponseViewModel>> Handle(
            GetLaptopDetailsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var laptop = await _laptopRepository.GetAll()
                    .Include(l => l.Brand)
                    .Include(l => l.Category)
                    .Include(l => l.Ports)
                    .Include(l => l.Warranties)
                    .Include(l => l.Images)
                    .Include(l => l.Variants.Where(v => v.IsActive))
                    .Include(l => l.Ratings)
                    .Where(l => l.Id == request.Id && !l.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (laptop == null)
                {
                    return RequestResponse<LaptopDetailsResponseViewModel>.Fail(
                        "Laptop not found",
                        "اللابتوب غير موجود"
                    );
                }

                if (!laptop.IsActive)
                {
                    return RequestResponse<LaptopDetailsResponseViewModel>.Fail(
                        "Laptop is not active",
                        "اللابتوب غير مفعل"
                    );
                }

                // Calculate statistics
                var statistics = await CalculateStatistics(laptop, cancellationToken);

                // Map to ViewModel
                var response = MapToViewModel(laptop, statistics);

                return RequestResponse<LaptopDetailsResponseViewModel>.Success(
                    response,
                    "Laptop details fetched successfully",
                    "تم جلب تفاصيل اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<LaptopDetailsResponseViewModel>.Fail(
                    "An error occurred while fetching laptop details",
                    "حدث خطأ أثناء جلب تفاصيل اللابتوب"
                );
            }
        }

        private async Task<StatisticsViewModel> CalculateStatistics(Laptop laptop, CancellationToken cancellationToken)
        {
            // Get ALL variant IDs for this laptop (including inactive)
            var allVariantIds = await _laptopVariantRepository.GetAll()
                .Where(lv => lv.LaptopId == laptop.Id)
                .Select(lv => lv.Id)
                .ToListAsync(cancellationToken);

            // Calculate total sales from order items for all laptop variants
            var totalSales = await _orderItemRepository.GetAll()
                .Where(oi => oi.ProductType == ProductType.LaptopVariant &&
                            allVariantIds.Contains(oi.ProductId))
                .CountAsync(cancellationToken);

            return new StatisticsViewModel
            {
                AverageRating = laptop.Ratings.Any() ? Math.Round(laptop.Ratings.Average(r => r.Stars), 1) : 0,
                TotalReviews = laptop.Ratings.Count,
                TotalSales = totalSales,
                ViewCount = 0 // You might want to track views separately
            };
        }
        private static LaptopDetailsResponseViewModel MapToViewModel(Laptop laptop, StatisticsViewModel statistics)
        {
            return new LaptopDetailsResponseViewModel
            {
                Id = laptop.Id,
                ModelName = laptop.ModelName,
                Brand = new BrandViewModel
                {
                    Id = laptop.Brand.Id,
                    Name = laptop.Brand.Name,
                    Country = laptop.Brand.Country ?? string.Empty,
                    LogoUrl = laptop.Brand.LogoUrl ?? string.Empty
                },
                Category = new CategoryViewModel
                {
                    Id = laptop.Category.Id,
                    Name = laptop.Category.Name,
                    Description = laptop.Category.Description ?? string.Empty
                },
                Processor = laptop.Processor,
                GPU = laptop.GPU ?? string.Empty,
                ScreenSize = laptop.ScreenSize ?? string.Empty,
                HasCamera = laptop.HasCamera,
                HasKeyboard = laptop.HasKeyboard,
                HasTouchScreen = laptop.HasTouchScreen,
                Description = laptop.Description ?? string.Empty,
                ReleaseYear = laptop.ReleaseYear,
                StoreLocation = laptop.StoreLocation ?? string.Empty,
                StoreContact = laptop.StoreContact ?? string.Empty,
                IsActive = laptop.IsActive,
                CreatedAt = laptop.CreatedAt,
                UpdatedAt = laptop.UpdatedAt,
                Ports = laptop.Ports.Select(p => new PortViewModel
                {
                    Id = p.Id,
                    Type = p.PortType,
                    Quantity = p.Quantity
                }).ToList(),
                Warranty = laptop.Warranties.FirstOrDefault() != null ? new WarrantyViewModel
                {
                    Id = laptop.Warranties.First().Id,
                    DurationMonths = laptop.Warranties.First().DurationMonths,
                    Type = laptop.Warranties.First().Type,
                    Coverage = laptop.Warranties.First().Coverage ?? string.Empty,
                    Provider = laptop.Warranties.First().Provider ?? string.Empty
                } : null,
                Images = laptop.Images
                    .OrderBy(i => i.DisplayOrder)
                    .Select(i => new ImageViewModel
                    {
                        Id = i.Id,
                        Url = i.ImageUrl,
                        IsMain = i.IsMain,
                        DisplayOrder = i.DisplayOrder
                    }).ToList(),
                Variants = laptop.Variants
                    .Where(v => v.IsActive)
                    .Select(v => new VariantViewModel
                    {
                        Id = v.Id,
                        Sku = v.SKU,
                        RAM = v.RAM,
                        Storage = v.StorageCapacityGB,
                        StorageType = v.StorageType,
                        CurrentPrice = v.CurrentPrice,
                        StockStatus = GetStockStatus(v)
                    }).ToList(),
                Statistics = statistics
            };
        }

        private static string GetStockStatus(TechZone.Domain.Entities.LaptopVariant variant)
        {
            var availableQuantity = variant.StockQuantity - variant.ReservedQuantity;

            if (availableQuantity <= 0) return "OutOfStock";
            if (availableQuantity <= variant.ReorderLevel) return "LowStock";
            return "InStock";
        }
    }
}