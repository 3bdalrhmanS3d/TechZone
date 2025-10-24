namespace TechZoneV1.Features.LaptopVariant.UpdateVariant.ViewModels
{
    public class UpdateLaptopVariantViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int LaptopId { get; set; }
        public int Ram { get; set; }
        public int Storage { get; set; }
        public string StorageType { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}