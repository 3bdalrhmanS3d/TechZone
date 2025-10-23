using MediatR;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Queries
{
    public class GetRecommendedLaptopVariantsQuery : IRequest<RequestResponse<PagedResult<RecommendedLaptopVariantViewModel>>>
    {
        public RecommendedLaptopVariantsQueryDto QueryDto { get; }

        public GetRecommendedLaptopVariantsQuery(RecommendedLaptopVariantsQueryDto queryDto)
        {
            QueryDto = queryDto;

        }
    }
}