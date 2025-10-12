using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Domain.Entities.Laptop
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