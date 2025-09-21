using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities.Laptop
{
    public class LaptopImage
    {
        public int Id { get; set; }
        public int LaptopId { get; set; }

        [Required, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Laptop Laptop { get; set; }
    }
}