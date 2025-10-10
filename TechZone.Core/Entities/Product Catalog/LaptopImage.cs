using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class LaptopImage : BaseEntity
    {
        public int LaptopId { get; set; }

        [Required, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Laptop Laptop { get; set; }
    }
}