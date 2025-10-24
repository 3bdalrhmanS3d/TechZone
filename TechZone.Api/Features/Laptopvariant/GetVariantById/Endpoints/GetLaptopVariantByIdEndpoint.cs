using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.GetVariantById.Queries;
using TechZoneV1.Features.LaptopVariant.GetVariantById.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantById.Endpoints
{
    public static class GetLaptopVariantByIdEndpoint
    {
        public static void MapGetLaptopVariantByIdEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptop-variants/{id}", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromRoute] int id) =>
            {
                var query = new GetLaptopVariantByIdQuery(id);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.NotFound(EndpointResponse<LaptopVariantDetailsViewModel>.NotFoundResponse(
                        result.Message,
                        result.MessageAr
                    ));
                }

                return Results.Ok(EndpointResponse<LaptopVariantDetailsViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetLaptopVariantById")
            .WithTags("Laptop Variants");
        }
    }
}