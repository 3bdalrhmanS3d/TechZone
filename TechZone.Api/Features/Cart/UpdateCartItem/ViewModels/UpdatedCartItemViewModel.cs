using System.Text.Json.Serialization;
using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Cart.UpdateCartItem.ViewModels
{
    public class UpdatedCartItemViewModel
    {
        public int Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}