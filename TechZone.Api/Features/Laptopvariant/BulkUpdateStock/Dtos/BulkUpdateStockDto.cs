using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Dtos
{
    public class BulkUpdateStockDto
    {
        [Required(ErrorMessage = "Updates are required")]
        [MinLength(1, ErrorMessage = "At least one update is required")]
        public List<StockUpdateItemDto> Updates { get; set; } = new();

        [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string? Reason { get; set; }
    }

    public class StockUpdateItemDto
    {
        [Required(ErrorMessage = "Variant ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Variant ID must be greater than 0")]
        public int VariantId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Operation is required")]
        [RegularExpression("add|subtract|set", ErrorMessage = "Operation must be 'add', 'subtract', or 'set'")]
        public string Operation { get; set; } = string.Empty;
    }
}