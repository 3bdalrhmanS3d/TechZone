using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.UpdateCartItem.Dtos
{
    // Request DTO
    public class UpdateCartItemRequestDto
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    // Response DTO (what the handler returns)
    public class UpdatedCartItemDto
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}