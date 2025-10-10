using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class UserDiscountUsage : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int DiscountId { get; set; }

        [Required]
        public int OrderId { get; set; }

        public DateTime UsedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public Discount Discount { get; set; }
        public Order Order { get; set; }
    }
}