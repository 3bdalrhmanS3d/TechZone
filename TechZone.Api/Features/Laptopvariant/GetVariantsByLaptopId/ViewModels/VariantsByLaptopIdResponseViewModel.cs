using TechZone.Domain.PagedResult;

namespace TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.ViewModels
{
    public class VariantsByLaptopIdResponseViewModel
    {
        public LaptopSummaryViewModel Laptop { get; set; } = new();
        public PagedResult<LaptopVariantSummaryViewModel> Variants { get; set; }
    }

    public class LaptopSummaryViewModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string Gpu { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public bool HasCamera { get; set; }
        public bool HasTouchScreen { get; set; }
    }

    public class LaptopVariantSummaryViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Ram { get; set; }
        public int Storage { get; set; }
        public string StorageType { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public string StockStatus { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}