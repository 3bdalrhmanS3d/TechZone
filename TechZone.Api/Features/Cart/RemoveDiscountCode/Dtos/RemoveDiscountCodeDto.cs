namespace TechZoneV1.Features.Cart.RemoveDiscountCode.Dtos
{
    // Response DTO (what the handler returns)
    public class RemoveDiscountCodeResponseDto
    {
        public CartSummaryRemoveDiscountDto CartSummary { get; set; } = new();
    }

    public class CartSummaryRemoveDiscountDto
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}