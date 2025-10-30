namespace TechZoneV1.Features.Cart.RemoveFromCart.Dtos
{
    // Response DTO (what the handler returns)
    public class RemoveFromCartResponseDto
    {
        public int RemovedItemId { get; set; }
        public CartSummaryRemoveDto CartSummary { get; set; }
    }

    public class CartSummaryRemoveDto
    {
        public int TotalItems { get; set; }
        public decimal Total { get; set; }
    }
}