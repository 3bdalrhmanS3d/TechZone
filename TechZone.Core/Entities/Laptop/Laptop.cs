using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class Laptop
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string ModelName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Processor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string GPU { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ScreenSize { get; set; } = string.Empty;

        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasTouchScreen { get; set; }

        [MaxLength(300)]
        public string Ports { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Warranty { get; set; } = string.Empty;

        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        // Navigation
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public ICollection<LaptopVariant> Variants { get; set; } = new List<LaptopVariant>();
        public ICollection<LaptopImage> Images { get; set; } = new List<LaptopImage>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }
}