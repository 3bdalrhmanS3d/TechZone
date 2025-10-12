using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.DTOs.Auth
{
    public class ConfirmEmailDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmation token is required")]
        public string Token { get; set; } = string.Empty;
    }

    public class ConfirmEmailWithCodeDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verification code is required")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Verification code must be between 4 and 10 characters")]
        public string Code { get; set; } = string.Empty;
    }
}
