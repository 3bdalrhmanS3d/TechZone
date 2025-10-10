using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class AuditLog : BaseEntity
    {
        public string? UserId { get; set; }

        [Required, MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string EntityType { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string EntityId { get; set; } = string.Empty;

        public string OldValues { get; set; } = string.Empty;
        public string NewValues { get; set; } = string.Empty;

        [MaxLength(50)]
        public string IpAddress { get; set; } = string.Empty;

        [MaxLength(500)]
        public string UserAgent { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser? User { get; set; }
    }
}