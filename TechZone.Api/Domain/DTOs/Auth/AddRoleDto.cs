using System.ComponentModel.DataAnnotations;

namespace TechZone.Domain.DTOs.Auth
{
    public class AddRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
