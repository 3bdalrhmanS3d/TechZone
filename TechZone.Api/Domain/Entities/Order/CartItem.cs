using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.Laptop;
using TechZone.Domain.Entities.User;

namespace TechZone.Domain.Entities.Order
{
    public class CartItem
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int LaptopVariantId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public LaptopVariant LaptopVariant { get; set; }
    }
}