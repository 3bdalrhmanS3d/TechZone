using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class AccessoryImage : BaseEntity
    {
        public int AccessoryId { get; set; }

        [Required, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Accessory Accessory { get; set; }
    }
}