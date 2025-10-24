using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Commands;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Dtos;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Endpoints
{
    public static class BulkUpdateStockEndpoint
    {
        public static void MapBulkUpdateStockEndpoint(this WebApplication app)
        {
            app.MapPost("/api/laptop-variants/bulk-stock-update", async (
                IMediator mediator,
                [FromBody] BulkUpdateStockDto bulkUpdateDto,
                CancellationToken cancellationToken) =>
            {
                // Validate the DTO
                if (!TryValidateBulkUpdateDto(bulkUpdateDto, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<BulkUpdateStockViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                var command = new BulkUpdateStockCommand(bulkUpdateDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<BulkUpdateStockViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Ok(EndpointResponse<BulkUpdateStockViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("BulkUpdateStock")
            .WithTags("Laptop Variants");
        }

        private static bool TryValidateBulkUpdateDto(BulkUpdateStockDto bulkUpdateDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate Updates list
            if (bulkUpdateDto.Updates == null || !bulkUpdateDto.Updates.Any())
            {
                errors.Add(nameof(bulkUpdateDto.Updates), new List<string> { "At least one update is required" });
                isValid = false;
            }
            else if (bulkUpdateDto.Updates.Count > 1000) // Limit to prevent abuse
            {
                errors.Add(nameof(bulkUpdateDto.Updates), new List<string> { "Cannot process more than 1000 updates at once" });
                isValid = false;
            }

            // Validate each update item
            if (bulkUpdateDto.Updates != null)
            {
                for (int i = 0; i < bulkUpdateDto.Updates.Count; i++)
                {
                    var update = bulkUpdateDto.Updates[i];
                    var prefix = $"{nameof(bulkUpdateDto.Updates)}[{i}]";

                    // Validate VariantId
                    if (update.VariantId <= 0)
                    {
                        errors.Add($"{prefix}.{nameof(update.VariantId)}", new List<string> { "Variant ID must be greater than 0" });
                        isValid = false;
                    }

                    // Validate Quantity
                    if (update.Quantity < 0)
                    {
                        errors.Add($"{prefix}.{nameof(update.Quantity)}", new List<string> { "Quantity must be a positive number" });
                        isValid = false;
                    }

                    // Validate Operation
                    if (string.IsNullOrWhiteSpace(update.Operation))
                    {
                        errors.Add($"{prefix}.{nameof(update.Operation)}", new List<string> { "Operation is required" });
                        isValid = false;
                    }
                    else if (!new[] { "add", "subtract", "set" }.Contains(update.Operation.ToLower()))
                    {
                        errors.Add($"{prefix}.{nameof(update.Operation)}", new List<string> { "Operation must be 'add', 'subtract', or 'set'" });
                        isValid = false;
                    }
                }
            }

            // Validate Reason length
            if (!string.IsNullOrEmpty(bulkUpdateDto.Reason) && bulkUpdateDto.Reason.Length > 500)
            {
                errors.Add(nameof(bulkUpdateDto.Reason), new List<string> { "Reason cannot exceed 500 characters" });
                isValid = false;
            }

            return isValid;
        }
    }
}