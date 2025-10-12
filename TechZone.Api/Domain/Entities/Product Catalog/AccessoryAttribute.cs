using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;

namespace TechZone.Domain.Entities
{
    public class AccessoryAttribute : BaseEntity
    {
        public int AccessoryId { get; set; }

        [Required, MaxLength(100)]
        public string AttributeKey { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string AttributeValue { get; set; } = string.Empty;

        // Navigation
        public Accessory Accessory { get; set; }
    }
}