using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.LaptopVariant.UpdateStock.Dtos
{
    public class UpdateStockDto
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Operation is required")]
        [RegularExpression("add|subtract|set", ErrorMessage = "Operation must be 'add', 'subtract', or 'set'")]
        public string Operation { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string? Reason { get; set; }
    }
}