using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos;

namespace TechZoneV1.Features.LaptopVariant.FilterVariants.Dtos
{
    public class FilterLaptopVariantsQueryDto : PaginationParamsDto<RecommendedLaptopVariantSortBy?>
    {
        public List<int>? BrandIds { get; set; }
        public List<int>? CategoryIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Processor { get; set; }
        public List<int>? Ram { get; set; }
        public List<int>? Storage { get; set; }
        public string? StorageType { get; set; }
        public List<string>? ScreenSize { get; set; }
        public bool? HasCamera { get; set; }
        public bool? HasTouchScreen { get; set; }
        public bool? InStock { get; set; }
        public bool? HasDiscount { get; set; }
        public decimal? MinRating { get; set; }
        public int? ReleaseYear { get; set; }
    }
}