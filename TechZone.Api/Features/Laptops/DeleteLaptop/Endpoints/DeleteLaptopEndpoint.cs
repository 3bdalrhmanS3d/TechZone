using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Laptops.DeleteLaptop.Commands;
using TechZoneV1.Features.Laptops.DeleteLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.DeleteLaptop.Endpoints
{
    public static class DeleteLaptopEndpoint
    {
        public static void MapDeleteLaptopEndpoint(this WebApplication app)
        {
            app.MapDelete("/api/laptops/{id}", async (
                IMediator mediator,
                [FromRoute] int id,
                CancellationToken cancellationToken) =>
            {
                // Validate ID
                if (id <= 0)
                {
                    return Results.BadRequest(EndpointResponse<LaptopDeletedViewModel>.ErrorResponse(
                        "Invalid laptop ID",
                        "معرف اللابتوب غير صالح",
                        400
                    ));
                }

                var command = new DeleteLaptopCommand(id);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<LaptopDeletedViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Ok(EndpointResponse<LaptopDeletedViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("DeleteLaptop")
            .WithTags("Laptops");
        }
    }
}