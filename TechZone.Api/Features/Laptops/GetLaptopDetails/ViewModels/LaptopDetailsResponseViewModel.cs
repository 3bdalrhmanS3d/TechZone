namespace TechZoneV1.Features.Laptops.GetLaptopDetails.ViewModels
{
    public class LaptopDetailsResponseViewModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public BrandViewModel Brand { get; set; } = new();
        public CategoryViewModel Category { get; set; } = new();
        public string Processor { get; set; } = string.Empty;
        public string GPU { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; } = true;
        public bool HasTouchScreen { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; }
        public string StoreLocation { get; set; } = string.Empty;
        public string StoreContact { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<PortViewModel> Ports { get; set; } = new();
        public WarrantyViewModel? Warranty { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
        public List<VariantViewModel> Variants { get; set; } = new();
        public StatisticsViewModel Statistics { get; set; } = new();
    }

    public class BrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class PortViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class WarrantyViewModel
    {
        public int Id { get; set; }
        public int DurationMonths { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Coverage { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
    }

    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class VariantViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string StorageType { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public string StockStatus { get; set; } = string.Empty;
    }

    public class StatisticsViewModel
    {
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int TotalSales { get; set; }
        public int ViewCount { get; set; }
    }
}