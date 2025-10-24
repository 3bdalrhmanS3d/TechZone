namespace TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels
{
    public class LaptopResponseViewModel
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
        public int? ReleaseYear { get; set; }
        public bool IsActive { get; set; }
        public int VariantCount { get; set; }
        public PriceRangeViewModel PriceRange { get; set; } = new();
        public double AverageRating { get; set; }
        public string MainImage { get; set; } = string.Empty;
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

    public class PriceRangeViewModel
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}