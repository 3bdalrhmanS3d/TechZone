using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;

namespace TechZone.Core.Entities.Order
{
    public class Discount
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Range(0, 100)]
        public decimal Percentage { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<LaptopVariant> LaptopVariants { get; set; } = new List<LaptopVariant>();
    }
}