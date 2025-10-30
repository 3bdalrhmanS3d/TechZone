namespace TechZoneV1.Features.Cart.RemoveDiscountCode.ViewModels
{
    public class RemoveDiscountCodeViewModel
    {
        public CartSummaryRemoveDiscountViewModel CartSummary { get; set; } = new();
    }

    public class CartSummaryRemoveDiscountViewModel
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}