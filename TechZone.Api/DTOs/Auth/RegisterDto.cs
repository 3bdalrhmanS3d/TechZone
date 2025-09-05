using System.ComponentModel.DataAnnotations;

namespace TechZone.Api.DTOs.Auth
{
    public class RegisterDto
    {
        public string? FullName { get; set; }
        [Required, StringLength(100)]
        public string UserName { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
    }
}
