using System.ComponentModel.DataAnnotations;

namespace TechZone.Domain.DTOs.Laptop
{
    public class CreateLaptopDto
    {
        [Required(ErrorMessage = "Model name is required")]
        [MaxLength(200, ErrorMessage = "Model name cannot exceed 200 characters")]
        public string ModelName { get; set; }

        [Required(ErrorMessage = "Processor is required")]
        [MaxLength(100, ErrorMessage = "Processor cannot exceed 100 characters")]
        public string Processor { get; set; }

        [Required(ErrorMessage = "GPU is required")]
        [MaxLength(100, ErrorMessage = "GPU cannot exceed 100 characters")]
        public string GPU { get; set; }

        [Required(ErrorMessage = "Screen size is required")]
        [MaxLength(50, ErrorMessage = "Screen size cannot exceed 50 characters")]
        public string ScreenSize { get; set; }

        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasTouchScreen { get; set; }

        [Required(ErrorMessage = "Ports information is required")]
        [MaxLength(300, ErrorMessage = "Ports information cannot exceed 300 characters")]
        public string Ports { get; set; }
    }
}
