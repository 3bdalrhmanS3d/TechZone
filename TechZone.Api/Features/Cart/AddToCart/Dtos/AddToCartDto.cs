using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.AddToCart.Dtos
{
    // Request DTO
    public class AddToCartRequestDto
    {
        [Required(ErrorMessage = "Product type is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be greater than 0")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    // Response DTO (what the handler returns)
    public class AddedCartItemDto
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime AddedAt { get; set; }
        public CartSummaryDto CartSummary { get; set; } = new();
    }

    public class CartSummaryDto
    {
        public int TotalItems { get; set; }
        public decimal Total { get; set; }
    }
}