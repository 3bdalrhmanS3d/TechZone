using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities.Feedback;
using TechZone.Core.Entities.Logging;
using TechZone.Core.Entities.Order;
using TechZone.Core.Entities.Repair;

namespace TechZone.Core.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User"; // Enum: User, Moderator, Admin

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public List<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
    }
}