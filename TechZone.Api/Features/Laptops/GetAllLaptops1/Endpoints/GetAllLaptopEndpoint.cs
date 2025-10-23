using MediatR;
using Mapster;
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
            app.MapGet("/api/test/laptops", async (IMediator mediator, CancellationToken cancellationToken) =>
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

                var pagedData = laptopRequestResponse.Data!;
                if (pagedData.Items == null || !pagedData.Items.Any())
                {
                    return EndpointResponse<PagedResult<LaptopResponseViewModel>>.SuccessResponse(
                        new PagedResult<LaptopResponseViewModel>(new List<LaptopResponseViewModel>(), 0)
                    );
                }

                // ✅ المابينج بعد config
                var mappedItems = pagedData.Items.Adapt<List<LaptopResponseViewModel>>();

                var responseViewModel = new PagedResult<LaptopResponseViewModel>(
                    mappedItems,
                    pagedData.TotalCount
                );

                return EndpointResponse<PagedResult<LaptopResponseViewModel>>.SuccessResponse(responseViewModel);
            })
            .WithName("GetAllLaptops")
            .WithTags("Laptops")
            .Produces<PagedResult<LaptopResponseViewModel>>(StatusCodes.Status200OK);
        }
    }
}
