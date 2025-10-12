using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.DTOs.Auth
{
    public class TokenRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
