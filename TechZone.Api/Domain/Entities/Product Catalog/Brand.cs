using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.Entities;

namespace TechZone.Domain.Entities
{
    public class Brand : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(500)]
        public string LogoUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
    }
}