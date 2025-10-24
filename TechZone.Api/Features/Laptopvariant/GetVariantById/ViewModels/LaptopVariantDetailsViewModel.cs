namespace TechZoneV1.Features.LaptopVariant.GetVariantById.ViewModels
{
    public class LaptopVariantDetailsViewModel
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Laptop details
        public LaptopDetailsViewModel Laptop { get; set; } = new();
        public BrandDetailsViewModel Brand { get; set; } = new();
        public CategoryDetailsViewModel Category { get; set; } = new();
        public List<PortViewModel> Ports { get; set; } = new();
        public WarrantyViewModel? Warranty { get; set; }
        public List<ImageViewModel> Images { get; set; } = new();
    }

    public class LaptopDetailsViewModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string Processor { get; set; } = string.Empty;
        public string Gpu { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasTouchScreen { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? ReleaseYear { get; set; }
        public string StoreLocation { get; set; } = string.Empty;
        public string StoreContact { get; set; } = string.Empty;
    }

    public class BrandDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }

    public class CategoryDetailsViewModel
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
}