using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class Rating : BaseEntity
    {
        public string UserId { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        public string Comment { get; set; } = string.Empty;
        public bool IsVerifiedPurchase { get; set; } = false;

        // Navigation
        public ApplicationUser User { get; set; }
    }
}