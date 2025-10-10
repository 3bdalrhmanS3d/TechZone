using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class StockAlert : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        public bool IsNotified { get; set; } = false;
        public DateTime? NotifiedAt { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
    }
}