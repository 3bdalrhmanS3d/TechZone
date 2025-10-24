using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Laptops.GetLaptopDetails.Queries;
using TechZoneV1.Features.Laptops.GetLaptopDetails.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetLaptopDetails.Endpoints
{
    public static class GetLaptopDetailsEndpoint
    {
        public static void MapGetLaptopDetailsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptops/{id}", async (
                IMediator mediator,
                [FromRoute] int id,
                CancellationToken cancellationToken) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest(EndpointResponse<LaptopDetailsResponseViewModel>.ErrorResponse(
                        "Invalid laptop ID",
                        "معرف اللابتوب غير صالح",
                        400
                    ));
                }

                var query = new GetLaptopDetailsQuery(id);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.NotFound(EndpointResponse<LaptopDetailsResponseViewModel>.NotFoundResponse(
                        result.Message,
                        result.MessageAr
                    ));
                }

                return Results.Ok(EndpointResponse<LaptopDetailsResponseViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetLaptopDetails")
            .WithTags("Laptops")
            .Produces<EndpointResponse<LaptopDetailsResponseViewModel>>(StatusCodes.Status200OK)
            .Produces<EndpointResponse<LaptopDetailsResponseViewModel>>(StatusCodes.Status400BadRequest)
            .Produces<EndpointResponse<LaptopDetailsResponseViewModel>>(StatusCodes.Status404NotFound);
        }
    }
}