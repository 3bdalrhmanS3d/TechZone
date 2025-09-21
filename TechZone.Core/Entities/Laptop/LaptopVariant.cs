using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class LaptopVariant
    {
        public int Id { get; set; }
        public int LaptopId { get; set; }
        public int? DiscountId { get; set; }

        [Range(0, int.MaxValue)]
        public int RAM { get; set; } // بالـ GB

        [Range(0, int.MaxValue)]
        public int Storage { get; set; } // بالـ GB أو TB

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        // Navigation
        public Laptop Laptop { get; set; }
        public Discount? Discount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}