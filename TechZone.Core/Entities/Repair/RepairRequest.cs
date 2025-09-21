using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities.User;

namespace TechZone.Core.Entities.Repair
{
    public enum RepairRequestStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public class RepairRequest
    {
        public int RequestId { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }
        public int? LaptopId { get; set; }

        public string Notes { get; set; } = string.Empty;

        [Required]
        public RepairRequestStatus Status { get; set; } = RepairRequestStatus.Pending;

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public RepairServiceItem RepairServiceItem { get; set; }
        public Laptop? Laptop { get; set; }
    }
}