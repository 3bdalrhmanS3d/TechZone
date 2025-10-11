using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ProfileImageUrl { get; set; } = string.Empty;

        [MaxLength(500)]
        public string AddressLine { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = "Egypt";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public ICollection<TechZone.Core.Entities.Order.Order> Orders { get; set; } = new List<TechZone.Core.Entities.Order.Order>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public ICollection<UserDiscountUsage> UserDiscountUsages { get; set; } = new List<UserDiscountUsage>();
        public ICollection<ProductView> ProductViews { get; set; } = new List<ProductView>();
        public ICollection<StockAlert> StockAlerts { get; set; } = new List<StockAlert>();
        public List<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
        public virtual ICollection<EmailQueue> EmailQueue { get; set; } = new List<EmailQueue>();
    }
}