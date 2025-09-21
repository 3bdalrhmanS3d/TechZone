using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities.User;

namespace TechZone.Core.Entities.Logging
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }

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