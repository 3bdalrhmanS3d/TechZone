using System.ComponentModel.DataAnnotations;

namespace TechZone.DTOs.Auth
{
    public class AddRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
