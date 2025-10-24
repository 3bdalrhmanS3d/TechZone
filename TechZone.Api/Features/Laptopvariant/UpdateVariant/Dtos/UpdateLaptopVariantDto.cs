using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.LaptopVariant.UpdateVariant.Dtos
{
    public class UpdateLaptopVariantDto
    {
        [MaxLength(100, ErrorMessage = "SKU cannot exceed 100 characters")]
        public string? Sku { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "RAM must be a positive number")]
        public int? Ram { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Storage must be a positive number")]
        public int? Storage { get; set; }

        [MaxLength(20, ErrorMessage = "Storage type cannot exceed 20 characters")]
        public string? StorageType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Current price must be a positive number")]
        public decimal? CurrentPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a positive number")]
        public int? StockQuantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reorder level must be a positive number")]
        public int? ReorderLevel { get; set; }

        public bool? IsActive { get; set; }
    }
}