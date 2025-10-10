using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public string UserId { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
    }
}