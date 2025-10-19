using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels
{
    public class LaptopResponseViewModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string GPU { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasTouchScreen { get; set; }
        public ICollection<LaptopPort> Ports { get; set; } = new List<LaptopPort>();
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public ICollection<LaptopWarranty> Warranty { get; set; } = new List<LaptopWarranty>();

        // Related data
        public BrandVM Brand { get; set; }
        public CategoryVM Category { get; set; }

        public ICollection<LaptopVariantVM> Variants { get; set; } = new List<LaptopVariantVM>();
        public ICollection<LaptopImageVM> Images { get; set; } = new List<LaptopImageVM>();

        public class BrandVM
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class CategoryVM
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        public class LaptopVariantVM
        {
            public int Id { get; set; }
            public int Ram { get; set; }
            public int Storage { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
        }

        public class LaptopImageVM
        {
            public int Id { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
            public bool IsMain { get; set; }
        }
    }
}
