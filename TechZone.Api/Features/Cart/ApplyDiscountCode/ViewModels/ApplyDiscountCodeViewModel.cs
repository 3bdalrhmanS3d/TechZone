using System.Text.Json.Serialization;
using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.ApplyDiscountCode.ViewModels
{
    public class ApplyDiscountCodeViewModel
    {
        public string Code { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiscountType DiscountType { get; set; }

        public decimal Value { get; set; }
        public decimal DiscountAmount { get; set; }
        public CartSummaryDiscountViewModel CartSummary { get; set; } = new();
    }

    public class CartSummaryDiscountViewModel
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}