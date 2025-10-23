namespace TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels
{
    public class RecommendedLaptopVariantViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Ram { get; set; }
        public int Storage { get; set; }
        public string StorageType { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public string StockStatus { get; set; } = string.Empty;
        public int ReorderLevel { get; set; }
        public bool IsActive { get; set; }

        // Laptop details
        public LaptopViewModel Laptop { get; set; } = new();
        public BrandViewModel Brand { get; set; } = new();
        public CategoryViewModel Category { get; set; } = new();

        public List<string> Images { get; set; } = new();
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }

    public class LaptopViewModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string Gpu { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public bool HasCamera { get; set; }
        public bool HasTouchScreen { get; set; }
        public string StoreLocation { get; set; } = string.Empty;
    }

    public class BrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}