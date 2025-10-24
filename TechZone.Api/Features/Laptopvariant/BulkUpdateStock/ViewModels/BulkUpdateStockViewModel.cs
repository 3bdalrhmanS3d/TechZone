namespace TechZoneV1.Features.LaptopVariant.BulkUpdateStock.ViewModels
{
    public class BulkUpdateStockViewModel
    {
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public List<StockUpdateResultViewModel> Results { get; set; } = new();
    }

    public class StockUpdateResultViewModel
    {
        public int VariantId { get; set; }
        public bool Success { get; set; }
        public int PreviousStock { get; set; }
        public int NewStock { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}