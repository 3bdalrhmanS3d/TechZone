using System.ComponentModel.DataAnnotations;

namespace TechZone.Api.DTOs.Auth
{
    public class TokenRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
