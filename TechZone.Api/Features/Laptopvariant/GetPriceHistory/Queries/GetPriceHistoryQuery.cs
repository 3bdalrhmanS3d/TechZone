using MediatR;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.Dtos;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetPriceHistory.Queries
{
    public class GetPriceHistoryQuery : IRequest<RequestResponse<PriceHistoryResponseViewModel>>
    {
        public int Id { get; }
        public PriceHistoryQueryDto QueryDto { get; }

        public GetPriceHistoryQuery(int id, PriceHistoryQueryDto queryDto)
        {
            Id = id;
            QueryDto = queryDto;
        }
    }
}