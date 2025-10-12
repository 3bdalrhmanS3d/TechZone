using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;

namespace TechZone.Domain.Entities.Repair
{
    public enum RepairType
    {
        Hardware,
        Software,
        Diagnostic,
        Upgrade,
        Other
    }

    public class RepairServiceItem : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public RepairType RepairType { get; set; }

        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }

        public int EstimatedDays { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }
}