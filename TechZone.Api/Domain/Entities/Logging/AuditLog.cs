using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;

namespace TechZone.Domain.Entities.Logging
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [Required, MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Entity { get; set; } = string.Empty;

        public int EntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Details { get; set; } = string.Empty;

        // Navigation
        public ApplicationUser User { get; set; }
    }
}