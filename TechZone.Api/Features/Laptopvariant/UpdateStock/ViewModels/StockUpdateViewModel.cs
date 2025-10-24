namespace TechZoneV1.Features.LaptopVariant.UpdateStock.ViewModels
{
    public class StockUpdateViewModel
    {
        public int Id { get; set; }
        public int PreviousStock { get; set; }
        public int NewStock { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public string StockStatus { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}