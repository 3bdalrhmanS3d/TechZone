using MediatR;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels;
using TechZoneV1.Features.LaptopVariant.FilterVariants.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.FilterVariants.Queries
{
    public class FilterLaptopVariantsQuery : IRequest<RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>>
    {
        public FilterLaptopVariantsQueryDto QueryDto { get; }

        public FilterLaptopVariantsQuery(FilterLaptopVariantsQueryDto queryDto)
        {
            QueryDto = queryDto;
        }
    }
}