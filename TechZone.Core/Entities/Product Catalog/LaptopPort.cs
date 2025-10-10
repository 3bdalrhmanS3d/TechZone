using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class LaptopPort : BaseEntity
    {
        public int LaptopId { get; set; }

        [Required, MaxLength(50)]
        public string PortType { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Quantity { get; set; } = 1;

        // Navigation
        public Laptop Laptop { get; set; }
    }
}