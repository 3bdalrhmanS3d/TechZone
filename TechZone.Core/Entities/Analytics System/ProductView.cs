using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class ProductView : BaseEntity
    {
        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string? UserId { get; set; }

        [MaxLength(50)]
        public string IpAddress { get; set; } = string.Empty;

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser? User { get; set; }
    }
}