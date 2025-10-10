using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public enum DiscountType
    {
        Percentage,
        FixedAmount
    }

    public class Discount : BaseEntity
    {
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DiscountType DiscountType { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Value { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MinimumPurchase { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MaxDiscountAmount { get; set; }

        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = new List<ProductDiscount>();
        public ICollection<UserDiscountUsage> UserDiscountUsages { get; set; } = new List<UserDiscountUsage>();
    }
}