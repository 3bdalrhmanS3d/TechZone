using System.Text.Json.Serialization;
using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.AddToCart.ViewModels
{
    public class AddedCartItemViewModel
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
        public CartSummaryViewModel CartSummary { get; set; } = new();
    }

    public class CartSummaryViewModel
    {
        public int TotalItems { get; set; }
        public decimal Total { get; set; }
    }
}