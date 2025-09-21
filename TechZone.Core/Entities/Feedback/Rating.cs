using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities;

namespace TechZone.Core.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LaptopId { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public Laptop Laptop { get; set; }
    }
}