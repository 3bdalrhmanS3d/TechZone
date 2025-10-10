using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.Entities.User;

namespace TechZone.Core.Entities.Repair
{
    public enum RepairType
    {
        Hardware,
        Software,
        Other
    }

    public enum RepairRequestStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public class RepairServiceItem
    {
        public int ItemId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public RepairType RepairType { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(100)]
        public string EstimatedTime { get; set; } = string.Empty;

        // Navigation
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }
}