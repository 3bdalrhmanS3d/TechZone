using TechZone.Domain.PagedResult;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Dtos
{
    public class LaptopQueryDto : PaginationParamsDto<FullLaptopSortBy>
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public int? ReleaseYear { get; set; }
        public bool? HasCamera { get; set; }
        public bool? HasTouchScreen { get; set; }
    }
}