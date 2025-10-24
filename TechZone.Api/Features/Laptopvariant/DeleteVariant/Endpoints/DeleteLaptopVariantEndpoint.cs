using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.DeleteVariant.Commands;
using TechZoneV1.Features.LaptopVariant.DeleteVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.DeleteVariant.Endpoints
{
    public static class DeleteLaptopVariantEndpoint
    {
        public static void MapDeleteLaptopVariantEndpoint(this WebApplication app)
        {
            app.MapDelete("/api/laptop-variants/{id}", async (
                IMediator mediator,
                [FromRoute] int id,
                CancellationToken cancellationToken) =>
            {
                // Validate ID
                if (id <= 0)
                {
                    return Results.BadRequest(EndpointResponse<DeleteLaptopVariantViewModel>.ErrorResponse(
                        "Invalid laptop variant ID",
                        "معرف نوع اللابتوب غير صالح",
                        400
                    ));
                }

                var command = new DeleteLaptopVariantCommand(id);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<DeleteLaptopVariantViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Ok(EndpointResponse<DeleteLaptopVariantViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("DeleteLaptopVariant")
            .WithTags("Laptop Variants");
        }
    }
}