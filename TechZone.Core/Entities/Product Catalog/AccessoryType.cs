using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class AccessoryType : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Navigation
        public ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();
    }
}