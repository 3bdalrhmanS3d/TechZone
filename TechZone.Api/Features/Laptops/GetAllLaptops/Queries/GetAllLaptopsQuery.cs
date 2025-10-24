using MediatR;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptops.GetAllLaptops.Dtos;
using TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Queries
{
    public class GetAllLaptopsQuery : IRequest<RequestResponse<PagedResult<LaptopResponseViewModel>>>
    {
        public LaptopQueryDto QueryDto { get; }

        public GetAllLaptopsQuery(LaptopQueryDto queryDto)
        {
            QueryDto = queryDto;
        }
    }
}