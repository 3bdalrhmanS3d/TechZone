using MediatR;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Dtos;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Queries
{
    public class GetVariantsByLaptopIdQuery : IRequest<RequestResponse<VariantsByLaptopIdResponseViewModel>>
    {
        public int LaptopId { get; }
        public VariantsByLaptopIdQueryDto QueryDto { get; }

        public GetVariantsByLaptopIdQuery(int laptopId, VariantsByLaptopIdQueryDto queryDto)
        {
            LaptopId = laptopId;
            QueryDto = queryDto;
        }
    }
}