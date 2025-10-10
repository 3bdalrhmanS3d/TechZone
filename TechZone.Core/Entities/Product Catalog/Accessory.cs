using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class Accessory : BaseEntity
    {
        [Required, MaxLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public int AccessoryTypeId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CurrentPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Range(0, int.MaxValue)]
        public int ReservedQuantity { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; } = 5;

        public bool IsActive { get; set; } = true;

        // Navigation
        public AccessoryType AccessoryType { get; set; }
        public ICollection<AccessoryImage> Images { get; set; } = new List<AccessoryImage>();
        public ICollection<AccessoryAttribute> Attributes { get; set; } = new List<AccessoryAttribute>();
        public ICollection<AccessoryCompatibility> CompatibleLaptops { get; set; } = new List<AccessoryCompatibility>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}