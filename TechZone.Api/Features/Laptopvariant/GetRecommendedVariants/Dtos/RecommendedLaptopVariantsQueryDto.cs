    using TechZone.Domain.PagedResult;
namespace TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos
{

        public class RecommendedLaptopVariantsQueryDto : PaginationParamsDto<RecommendedLaptopVariantSortBy>
        {
            public int? CategoryId { get; set; }
            public int? BrandId { get; set; }
        }
}
