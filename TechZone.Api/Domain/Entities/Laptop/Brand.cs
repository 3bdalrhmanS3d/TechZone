using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Domain.Entities
{
    public class Brand
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(500)]
        public string LogoUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Navigation
        public ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
    }
}