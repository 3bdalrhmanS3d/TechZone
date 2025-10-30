using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.ApplyDiscountCode.Dtos
{
    // Request DTO
    public class ApplyDiscountCodeRequestDto
    {
        [Required(ErrorMessage = "Discount code is required")]
        [MaxLength(50, ErrorMessage = "Discount code cannot exceed 50 characters")]
        public string Code { get; set; } = string.Empty;
    }

    // Response DTO (what the handler returns)
    public class ApplyDiscountCodeResponseDto
    {
        public string Code { get; set; } = string.Empty;
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public decimal DiscountAmount { get; set; }
        public CartSummaryDiscountDto CartSummary { get; set; } = new();
    }

    public class CartSummaryDiscountDto
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}