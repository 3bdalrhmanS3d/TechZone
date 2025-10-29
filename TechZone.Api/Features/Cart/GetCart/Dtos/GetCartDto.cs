using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.GetCart.Dtos
{
    // Request DTO (empty for now, for pattern consistency)
    public class GetCartRequestDto
    {
        // Can be extended with additional parameters if needed
    }

    // Response DTO (what the handler returns)
    public class CartDto
    {
        public List<CartItemDto> Items { get; set; } = new();
        public int TotalItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
        public string? AppliedDiscountCode { get; set; }
    }

    public class CartItemDto
    {
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public int StockAvailable { get; set; }
        public string Image { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }
}