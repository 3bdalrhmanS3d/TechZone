namespace TechZoneV1.Features.Cart.RemoveFromCart.ViewModels
{
    public class RemoveFromCartViewModel
    {
        public int RemovedItemId { get; set; }
        public CartSummaryUpdateViewModel CartSummary { get; set; } 
    }

    public class CartSummaryUpdateViewModel
    {
        public int TotalItems { get; set; }
        public decimal Total { get; set; }
    }
}