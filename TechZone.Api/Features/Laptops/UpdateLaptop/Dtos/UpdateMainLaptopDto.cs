using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.Laptops.UpdateLaptop.Dtos
{
    public class UpdateMainLaptopDto
    {
        [MaxLength(200, ErrorMessage = "Model name cannot exceed 200 characters")]
        public string? ModelName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Brand ID must be greater than 0")]
        public int? BrandId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be greater than 0")]
        public int? CategoryId { get; set; }

        [MaxLength(100, ErrorMessage = "Processor cannot exceed 100 characters")]
        public string? Processor { get; set; }

        [MaxLength(100, ErrorMessage = "GPU cannot exceed 100 characters")]
        public string? GPU { get; set; }

        [MaxLength(50, ErrorMessage = "Screen size cannot exceed 50 characters")]
        public string? ScreenSize { get; set; }

        public bool? HasCamera { get; set; }
        public bool? HasKeyboard { get; set; }
        public bool? HasTouchScreen { get; set; }

        public string? Description { get; set; }

        [MaxLength(200, ErrorMessage = "Store location cannot exceed 200 characters")]
        public string? StoreLocation { get; set; }

        [MaxLength(100, ErrorMessage = "Store contact cannot exceed 100 characters")]
        public string? StoreContact { get; set; }

        [Range(2000, 2030, ErrorMessage = "Release year must be between 2000 and 2030")]
        public int? ReleaseYear { get; set; }

        public bool? IsActive { get; set; }
    }
}