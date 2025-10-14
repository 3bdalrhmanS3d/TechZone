using TechZone.Domain.Entities;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Dtos
{
    public class LaptopsDto
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
        public BrandDTO Brand { get; set; }
        public CategoryDTO Category { get; set; }

        public ICollection<LaptopVariantDTO> Variants { get; set; } = new List<LaptopVariantDTO>();
        public ICollection<LaptopImageDTO> Images { get; set; } = new List<LaptopImageDTO>();
    }

    public class BrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class LaptopVariantDTO
    {
        public int Id { get; set; }
        public int Ram { get; set; }
        public int Storage { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class LaptopImageDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}

