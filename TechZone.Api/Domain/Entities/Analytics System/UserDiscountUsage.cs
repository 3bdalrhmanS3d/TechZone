using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;

namespace TechZone.Domain.Entities
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