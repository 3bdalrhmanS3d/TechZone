using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.UpdateStock.Commands;
using TechZoneV1.Features.LaptopVariant.UpdateStock.Dtos;
using TechZoneV1.Features.LaptopVariant.UpdateStock.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.UpdateStock.Endpoints
{
    public static class UpdateStockEndpoint
    {
        public static void MapUpdateStockEndpoint(this WebApplication app)
        {
            app.MapPatch("/api/laptop-variants/{id}/stock", async (
                IMediator mediator,
                [FromRoute] int id,
                [FromBody] UpdateStockDto updateDto,
                CancellationToken cancellationToken) =>
            {
                // Validate the DTO
                if (!TryValidateUpdateDto(updateDto, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<StockUpdateViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                var command = new UpdateStockCommand(id, updateDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<StockUpdateViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Ok(EndpointResponse<StockUpdateViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("UpdateStock")
            .WithTags("Laptop Variants");
        }

        private static bool TryValidateUpdateDto(UpdateStockDto updateDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate Quantity
            if (updateDto.Quantity < 0)
            {
                errors.Add(nameof(updateDto.Quantity), new List<string> { "Quantity must be a positive number" });
                isValid = false;
            }

            // Validate Operation
            if (string.IsNullOrWhiteSpace(updateDto.Operation))
            {
                errors.Add(nameof(updateDto.Operation), new List<string> { "Operation is required" });
                isValid = false;
            }
            else if (!new[] { "add", "subtract", "set" }.Contains(updateDto.Operation.ToLower()))
            {
                errors.Add(nameof(updateDto.Operation), new List<string> { "Operation must be 'add', 'subtract', or 'set'" });
                isValid = false;
            }

            // Validate Reason length
            if (!string.IsNullOrEmpty(updateDto.Reason) && updateDto.Reason.Length > 500)
            {
                errors.Add(nameof(updateDto.Reason), new List<string> { "Reason cannot exceed 500 characters" });
                isValid = false;
            }

            return isValid;
        }
    }
}