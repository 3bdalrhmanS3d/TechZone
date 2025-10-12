using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.Repair;

namespace TechZone.Domain.Entities
{
    public class LaptopVariant : BaseEntity
    {
        public int LaptopId { get; set; }

        [Required, MaxLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int RAM { get; set; }

        [Range(0, int.MaxValue)]
        public int StorageCapacityGB { get; set; }

        [Required, MaxLength(20)]
        public string StorageType { get; set; } = string.Empty;

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
        public Laptop Laptop { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }
}