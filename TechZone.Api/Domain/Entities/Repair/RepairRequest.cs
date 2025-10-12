using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;

namespace TechZone.Domain.Entities.Repair
{
    public enum RepairRequestStatus
    {
        Pending,
        Diagnosed,
        Approved,
        InProgress,
        Completed,
        Cancelled,
        PickupReady
    }

    public enum RepairPriority
    {
        Low,
        Normal,
        High,
        Urgent
    }

    public class RepairRequest : BaseEntity
    {
        [Required, MaxLength(50)]
        public string RequestNumber { get; set; } = string.Empty;

        public string UserId { get; set; }
        public int ServiceItemId { get; set; }
        public int? LaptopVariantId { get; set; }

        [MaxLength(100)]
        public string DeviceSerial { get; set; } = string.Empty;

        [Required]
        public string IssueDescription { get; set; } = string.Empty;

        public string DiagnosisNotes { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal QuotedPrice { get; set; }

        [Required]
        public RepairRequestStatus Status { get; set; } = RepairRequestStatus.Pending;

        [Required]
        public RepairPriority Priority { get; set; } = RepairPriority.Normal;

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedDate { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public RepairServiceItem ServiceItem { get; set; }
        public LaptopVariant? LaptopVariant { get; set; }
    }
}