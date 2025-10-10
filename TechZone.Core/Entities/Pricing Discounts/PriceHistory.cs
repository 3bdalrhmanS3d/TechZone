using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public enum ProductType
    {
        LaptopVariant,
        Accessory
    }

    public class PriceHistory : BaseEntity
    {
        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal OldPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal NewPrice { get; set; }

        [MaxLength(100)]
        public string ChangeReason { get; set; } = string.Empty;

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
        public DateTime? EffectiveTo { get; set; }

        public string ChangedByUserId { get; set; }

        // Navigation
        public ApplicationUser ChangedByUser { get; set; }
    }
}