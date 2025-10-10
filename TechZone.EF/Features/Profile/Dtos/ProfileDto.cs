using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.EF.Features.Profile.Dtos
{
    public class ProfileDto
    {
        public string Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime? LastUpdated { get; set; } = DateTime.Now;

        //public string? Phone { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

    }
}
