using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;

namespace TechZone.Domain.Entities
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