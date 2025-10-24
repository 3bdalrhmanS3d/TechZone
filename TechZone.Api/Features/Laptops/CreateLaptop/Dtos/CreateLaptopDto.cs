using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.Laptops.CreateLaptop.Dtos
{
    public class CreateFullLaptopDto
    {
        public string ModelName { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Processor { get; set; } = string.Empty;
        public string GPU { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public bool HasCamera { get; set; } = true;
        public bool HasKeyboard { get; set; } = true;
        public bool HasTouchScreen { get; set; }

        public string Description { get; set; } = string.Empty;
        public string StoreLocation { get; set; } = string.Empty;

        public string StoreContact { get; set; } = string.Empty;

        public int? ReleaseYear { get; set; }
        public bool IsActive { get; set; } = true;
    }
}