using MediatR;
using TechZone.Domain.PagedResult;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Laptops.GetAllLaptops.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Queries
{
    public record GetAllLaptopsQuery : IRequest<RequestResponse<PagedResult<LaptopsDto>>>;

}
