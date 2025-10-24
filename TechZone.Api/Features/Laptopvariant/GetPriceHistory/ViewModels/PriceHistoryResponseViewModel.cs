using TechZone.Domain.PagedResult;

namespace TechZoneV1.Features.LaptopVariant.GetPriceHistory.ViewModels
{
    public class PriceHistoryResponseViewModel
    {
        public VariantViewModel Variant { get; set; } = new();
        public PagedResult<PriceHistoryItemViewModel> PriceHistory { get; set; } 
        public PriceStatisticsViewModel Statistics { get; set; } = new();
    }

    public class VariantViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
    }

    public class PriceHistoryItemViewModel
    {
        public int Id { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string ChangeReason { get; set; } = string.Empty;
        public decimal ChangePercentage { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public UserViewModel ChangedBy { get; set; } = new();
        public bool IsCurrentPrice { get; set; }
    }

    public class UserViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }

    public class PriceStatisticsViewModel
    {
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public int TotalChanges { get; set; }
    }
}