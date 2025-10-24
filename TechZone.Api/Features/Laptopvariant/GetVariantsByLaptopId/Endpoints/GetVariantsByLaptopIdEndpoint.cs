using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Dtos;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Queries;
using TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Endpoints
{
    public static class GetVariantsByLaptopIdEndpoint
    {
        public static void MapGetVariantsByLaptopIdEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptops/{laptopId}/variants", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromRoute] int laptopId,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10,
                [FromQuery] bool inStockOnly = false) =>
            {
                var queryDto = new VariantsByLaptopIdQueryDto
                {
                    Page = page,
                    PageSize = pageSize,
                    InStockOnly = inStockOnly
                };

                var query = new GetVariantsByLaptopIdQuery(laptopId, queryDto);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.NotFound(EndpointResponse<VariantsByLaptopIdResponseViewModel>.NotFoundResponse(
                        result.Message,
                        result.MessageAr
                    ));
                }

                return Results.Ok(EndpointResponse<VariantsByLaptopIdResponseViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetVariantsByLaptopId")
            .WithTags("Laptop Variants");
        }
    }
}