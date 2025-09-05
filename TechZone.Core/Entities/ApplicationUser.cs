using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Entities;

namespace TechZone.Core.models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        public ICollection<Order> Orders { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
