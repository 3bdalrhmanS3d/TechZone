using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Core.Entities.Feedback;
using TechZone.Core.Entities.Repair;
using TechZone.Core.Entities.User;

namespace TechZone.Core.Entities.Laptop
{
    public class Laptop : BaseEntity
    {
        [Required, MaxLength(200)]
        public string ModelName { get; set; } = string.Empty;

        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string Processor { get; set; } = string.Empty;

        [MaxLength(100)]
        public string GPU { get; set; } = string.Empty;

        [MaxLength(50)]
        public string ScreenSize { get; set; } = string.Empty;

        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; } = true;
        public bool HasTouchScreen { get; set; }

        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string StoreLocation { get; set; } = string.Empty;

        [MaxLength(100)]
        public string StoreContact { get; set; } = string.Empty;

        public int? ReleaseYear { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public ICollection<LaptopVariant> Variants { get; set; } = new List<LaptopVariant>();
        public ICollection<LaptopImage> Images { get; set; } = new List<LaptopImage>();
        public ICollection<LaptopPort> Ports { get; set; } = new List<LaptopPort>();
        public ICollection<LaptopWarranty> Warranties { get; set; } = new List<LaptopWarranty>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<AccessoryCompatibility> CompatibleAccessories { get; set; } = new List<AccessoryCompatibility>();
        public ICollection<RepairRequest> RepairRequests { get; set; } = new List<RepairRequest>();
    }

    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LaptopId { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public Laptop Laptop { get; set; }
    }

    public class RepairRequest
    {
        public int RequestId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ItemId { get; set; }
        public int? LaptopId { get; set; }

        public string Notes { get; set; } = string.Empty;

        [Required]
        public RepairRequestStatus Status { get; set; } = RepairRequestStatus.Pending;

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser User { get; set; }
        public RepairServiceItem RepairServiceItem { get; set; }
        public Laptop? Laptop { get; set; }
    }
}