using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.Entities.User;

namespace TechZone.Core.Entities.Order
{
    public class CartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LaptopVariantId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public LaptopVariant LaptopVariant { get; set; }
    }
}