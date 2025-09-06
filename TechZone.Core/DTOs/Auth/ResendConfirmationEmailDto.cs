using System.ComponentModel.DataAnnotations;

namespace TechZone.Api.DTOs.Auth
{
    public class ResendConfirmationEmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}
