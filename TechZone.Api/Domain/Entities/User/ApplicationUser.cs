using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.Laptop;
using TechZone.Domain.Entities.Logging;
using TechZone.Domain.Entities.Order;
using TechZone.Domain.Entities.Repair;

namespace TechZone.Domain.Entities.User
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
        public ICollection<TechZone.Domain.Entities.Order.Order> Orders { get; set; } = new List<TechZone.Domain.Entities.Order.Order>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public List<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
        public virtual ICollection<EmailQueue> EmailQueue { get; set; }
    }
}