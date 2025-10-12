using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;

namespace TechZone.DTOs.Auth
{
    public class ResendConfirmationEmailDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }

    public class ResendVerificationCodeDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verification type is required")]
        public VerificationCodeType VerificationType { get; set; }  // "EmailVerification" or "PasswordReset"
    }
}
