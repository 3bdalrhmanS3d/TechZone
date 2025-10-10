using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class LaptopWarranty : BaseEntity
    {
        public int LaptopId { get; set; }

        [Required]
        public int DurationMonths { get; set; }

        [Required, MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        public string Coverage { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Provider { get; set; } = string.Empty;

        // Navigation
        public Laptop Laptop { get; set; }
    }
}