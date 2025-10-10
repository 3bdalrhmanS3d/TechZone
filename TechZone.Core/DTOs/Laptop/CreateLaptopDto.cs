using System.ComponentModel.DataAnnotations;

namespace TechZone.Api.DTOs.Laptop
{
    public class CreateLaptopDto
    {
        [Required(ErrorMessage = "Model name is required")]
        [MaxLength(200, ErrorMessage = "Model name cannot exceed 200 characters")]
        public string ModelName { get; set; } = null!;

        [Required(ErrorMessage = "Processor is required")]
        [MaxLength(100, ErrorMessage = "Processor cannot exceed 100 characters")]
        public string Processor { get; set; } = null!;

        [Required(ErrorMessage = "GPU is required")]
        [MaxLength(100, ErrorMessage = "GPU cannot exceed 100 characters")]
        public string GPU { get; set; } = null!;

        [Required(ErrorMessage = "Screen size is required")]
        [MaxLength(50, ErrorMessage = "Screen size cannot exceed 50 characters")]
        public string ScreenSize { get; set; } = null!;

        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasTouchScreen { get; set; }

        // Removed Ports property since it's now handled by LaptopPort entity
        // [Required(ErrorMessage = "Ports information is required")]
        // [MaxLength(300, ErrorMessage = "Ports information cannot exceed 300 characters")]
        // public string Ports { get; set; }

        // New properties for the updated schema
        //[Required(ErrorMessage = "Brand ID is required")]
        public int? BrandId { get; set; }

        //[Required(ErrorMessage = "Category ID is required")]
        public int? CategoryId { get; set; }

        public string? Description { get; set; }

        public int? ReleaseYear { get; set; }

        public string? StoreContact { get; set; }

        public string? StoreLocation { get; set; }
    }
}