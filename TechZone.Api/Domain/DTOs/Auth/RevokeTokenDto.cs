using System.ComponentModel.DataAnnotations;

namespace TechZone.Domain.DTOs.Auth
{
    public class RevokeTokenDto
    {
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
