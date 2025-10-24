namespace TechZoneV1.Features.Laptops.UpdateLaptop.ViewModels
{
    public class MainLaptopUpdatedViewModel
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
        public DateTime UpdatedAt { get; set; }
    }

    public class BrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}