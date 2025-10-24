using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.LaptopVariant.CreateVariant.Dtos
{
    public class CreateLaptopVariantDto
    {
        [Required(ErrorMessage = "Laptop ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Laptop ID must be greater than 0")]
        public int LaptopId { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        [MaxLength(100, ErrorMessage = "SKU cannot exceed 100 characters")]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "RAM is required")]
        [Range(0, int.MaxValue, ErrorMessage = "RAM must be a positive number")]
        public int Ram { get; set; }

        [Required(ErrorMessage = "Storage is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Storage must be a positive number")]
        public int Storage { get; set; }

        [Required(ErrorMessage = "Storage type is required")]
        [MaxLength(20, ErrorMessage = "Storage type cannot exceed 20 characters")]
        public string StorageType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Current price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Current price must be a positive number")]
        public decimal CurrentPrice { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a positive number")]
        public int StockQuantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reorder level must be a positive number")]
        public int ReorderLevel { get; set; } = 5;

        public bool IsActive { get; set; } = true;
    }
}