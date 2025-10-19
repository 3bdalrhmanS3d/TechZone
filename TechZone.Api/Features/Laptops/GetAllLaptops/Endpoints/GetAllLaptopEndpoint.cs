using MediatR;
using MapsterMapper;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptops.GetAllLaptops.Queries;
using TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Endpoints
{
    public static class GetAllLaptopEndpoint
    {
        public static void MapGetAllLaptopEndpoint(this WebApplication app)
        {
            app.MapGet("/api/test/laptops", async (IMediator mediator, IMapper mapper, CancellationToken cancellationToken) =>
            {
                var query = new GetAllLaptopsQuery();
                var laptopRequestResponse = await mediator.Send(query, cancellationToken);

                if (!laptopRequestResponse.IsSuccess)
                {
                    return EndpointResponse<PagedResult<LaptopResponseViewModel>>.ErrorResponse(
                        laptopRequestResponse.Message,
                        laptopRequestResponse.MessageAr
                    );
                }

                // here we map the DTOs to ViewModels using Mapster

                var laptopDtos = laptopRequestResponse.Data!;
                var viewModelPaged = mapper.Map<PagedResult<LaptopResponseViewModel>>(laptopDtos);

                return EndpointResponse<PagedResult<LaptopResponseViewModel>>.SuccessResponse(viewModelPaged);
            })
            .WithName("GetAllLaptops")
            .WithTags("Laptops")
            .Produces<List<LaptopResponseViewModel>>(StatusCodes.Status200OK);
        }
    }
}
