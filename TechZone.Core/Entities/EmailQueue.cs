using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.Entities
{
    public enum EmailStatus
    {
        Pending,
        Sent,
        Failed,
        Expired,
        Retrying
    }

    public enum EmailType
    {
        Verification,
        PasswordReset,
        Welcome,
        Notification,
        Promotion,
        Alert
    }

    public enum EmailPriority
    {
        High,
        Normal,
        Low
    }

    public class EmailQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ToEmail { get; set; } = string.Empty;

        public string? ToName { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public bool IsHtml { get; set; } = true;

        public EmailType EmailType { get; set; } = EmailType.Alert; // Verification, PasswordReset, Welcome, etc.

        public EmailStatus Status { get; set; } = EmailStatus.Pending; // Pending, Sent, Failed, Expired

        public int RetryCount { get; set; } = 0;
        public int MaxRetries { get; set; } = 3;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? NextRetryAt { get; set; }
        public string? ErrorMessage { get; set; }
        public EmailPriority Priority { get; set; } = EmailPriority.Normal; // High, Normal, Low

        // Related User (optional)
        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public string? TemplateData { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
