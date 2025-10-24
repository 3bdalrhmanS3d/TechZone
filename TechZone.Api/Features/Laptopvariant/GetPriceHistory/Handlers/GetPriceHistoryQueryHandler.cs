using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.Queries;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetPriceHistory.Handlers
{
    public class GetPriceHistoryQueryHandler : IRequestHandler<GetPriceHistoryQuery, RequestResponse<PriceHistoryResponseViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<PriceHistory> _priceHistoryRepository;

        public GetPriceHistoryQueryHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<PriceHistory> priceHistoryRepository)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _priceHistoryRepository = priceHistoryRepository;
        }

        public async Task<RequestResponse<PriceHistoryResponseViewModel>> Handle(
            GetPriceHistoryQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var queryDto = request.QueryDto;

                // Get laptop variant basic info
                var variant = await _laptopVariantRepository.GetAll()
                    .Where(lv => lv.Id == request.Id && lv.IsActive)
                    .Select(lv => new VariantViewModel
                    {
                        Id = lv.Id,
                        Sku = lv.SKU,
                        CurrentPrice = lv.CurrentPrice
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (variant == null)
                {
                    return RequestResponse<PriceHistoryResponseViewModel>.Fail(
                        "Laptop variant not found",
                        "نوع اللابتوب غير موجود"
                    );
                }

                // Base query for price history
                var baseQuery = _priceHistoryRepository.GetAll()
                    .Include(ph => ph.ChangedByUser)
                    .Where(ph => ph.ProductType == ProductType.LaptopVariant &&
                                 ph.ProductId == request.Id);

                // Apply days filter if provided
                if (queryDto.Days.HasValue)
                {
                    var startDate = DateTime.UtcNow.AddDays(-queryDto.Days.Value);
                    baseQuery = baseQuery.Where(ph => ph.EffectiveFrom >= startDate);
                }

                // Order by effective date (newest first)
                baseQuery = baseQuery.OrderByDescending(ph => ph.EffectiveFrom);

                // Get total count for pagination
                var totalCount = await baseQuery.CountAsync(cancellationToken);

                // Apply pagination and get data
                var priceHistories = await baseQuery
                    .Skip((queryDto.Page - 1) * queryDto.PageSize)
                    .Take(queryDto.PageSize)
                    .ToListAsync(cancellationToken);

                // Get current price history entry (the most recent one)
                var currentPriceHistory = await baseQuery
                    .FirstOrDefaultAsync(ph => ph.EffectiveTo == null, cancellationToken);

                // Calculate statistics from all records (not just the current page)
                var allPriceHistories = await baseQuery.ToListAsync(cancellationToken);
                var statistics = CalculateStatistics(allPriceHistories, variant.CurrentPrice);

                // Map to ViewModel
                var priceHistoryItems = priceHistories.Select(ph => new PriceHistoryItemViewModel
                {
                    Id = ph.Id,
                    OldPrice = ph.OldPrice,
                    NewPrice = ph.NewPrice,
                    ChangeReason = ph.ChangeReason,
                    ChangePercentage = CalculateChangePercentage(ph.OldPrice, ph.NewPrice),
                    EffectiveFrom = ph.EffectiveFrom,
                    EffectiveTo = ph.EffectiveTo,
                    ChangedBy = new UserViewModel
                    {
                        UserId = ph.ChangedByUserId,
                        UserName = ph.ChangedByUser?.UserName ?? "System"
                    },
                    IsCurrentPrice = ph.Id == currentPriceHistory?.Id
                }).ToList();

                var pagedResult = new PagedResult<PriceHistoryItemViewModel>(
                    priceHistoryItems,
                    totalCount,
                    queryDto.Page,
                    queryDto.PageSize
                );

                var response = new PriceHistoryResponseViewModel
                {
                    Variant = variant,
                    PriceHistory = pagedResult,
                    Statistics = statistics
                };

                return RequestResponse<PriceHistoryResponseViewModel>.Success(
                    response,
                    "Price history fetched successfully",
                    "تم جلب سجل الأسعار بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<PriceHistoryResponseViewModel>.Fail(
                    "An error occurred while fetching price history",
                    "حدث خطأ أثناء جلب سجل الأسعار"
                );
            }
        }

        private static decimal CalculateChangePercentage(decimal oldPrice, decimal newPrice)
        {
            if (oldPrice == 0) return 0;
            return Math.Round(((newPrice - oldPrice) / oldPrice) * 100, 2);
        }

        private static PriceStatisticsViewModel CalculateStatistics(List<PriceHistory> priceHistories, decimal currentPrice)
        {
            if (!priceHistories.Any())
            {
                return new PriceStatisticsViewModel
                {
                    LowestPrice = currentPrice,
                    HighestPrice = currentPrice,
                    AveragePrice = currentPrice,
                    TotalChanges = 0
                };
            }

            var allPrices = priceHistories
                .SelectMany(ph => new[] { ph.OldPrice, ph.NewPrice })
                .Distinct()
                .ToList();

            // Include current price in statistics
            allPrices.Add(currentPrice);

            return new PriceStatisticsViewModel
            {
                LowestPrice = allPrices.Min(),
                HighestPrice = allPrices.Max(),
                AveragePrice = Math.Round(allPrices.Average(), 2),
                TotalChanges = priceHistories.Count
            };
        }
    }
}