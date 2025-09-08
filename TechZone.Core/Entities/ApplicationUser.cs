using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Entities;

namespace TechZone.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public List<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();
    }
}
