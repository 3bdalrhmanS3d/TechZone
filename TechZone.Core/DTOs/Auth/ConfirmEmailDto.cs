using System.ComponentModel.DataAnnotations;

namespace TechZone.Api.DTOs.Auth
{
    public class ConfirmEmailDto
    {
        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmation token is required")]
        public string Token { get; set; } = string.Empty;
    }

    public class EmailConfirmationResultDto
    {
        public bool IsConfirmed { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime ConfirmationTime { get; set; } = DateTime.UtcNow;
        public string Email { get; set; } = string.Empty;
    }
}
