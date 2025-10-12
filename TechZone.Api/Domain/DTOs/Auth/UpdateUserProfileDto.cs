using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.DTOs.Auth
{
    public class UpdateUserProfileDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }
    }
}
