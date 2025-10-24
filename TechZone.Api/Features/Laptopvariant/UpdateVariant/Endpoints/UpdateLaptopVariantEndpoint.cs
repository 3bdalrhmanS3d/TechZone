using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.Commands;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.Dtos;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.UpdateVariant.Endpoints
{
    public static class UpdateLaptopVariantEndpoint
    {
        public static void MapUpdateLaptopVariantEndpoint(this WebApplication app)
        {
            app.MapPut("/api/laptop-variants/{id}", async (
                IMediator mediator,
                [FromRoute] int id,
                [FromBody] UpdateLaptopVariantDto updateDto,
                CancellationToken cancellationToken) =>
            {
                // Validate the DTO
                if (!TryValidateUpdateDto(updateDto, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<UpdateLaptopVariantViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                // Check if at least one property is provided
                if (!IsAnyPropertyProvided(updateDto))
                {
                    return Results.BadRequest(EndpointResponse<UpdateLaptopVariantViewModel>.ErrorResponse(
                        "At least one property must be provided for update",
                        "يجب تقديم خاصية واحدة على الأقل للتحديث",
                        400
                    ));
                }

                var command = new UpdateLaptopVariantCommand(id, updateDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<UpdateLaptopVariantViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Ok(EndpointResponse<UpdateLaptopVariantViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("UpdateLaptopVariant")
            .WithTags("Laptop Variants");
        }

        private static bool TryValidateUpdateDto(UpdateLaptopVariantDto updateDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate SKU only if provided
            if (!string.IsNullOrEmpty(updateDto.Sku))
            {
                if (updateDto.Sku.Length > 100)
                {
                    errors.Add(nameof(updateDto.Sku), new List<string> { "SKU cannot exceed 100 characters" });
                    isValid = false;
                }
            }

            // Validate RAM only if provided
            if (updateDto.Ram.HasValue && updateDto.Ram < 0)
            {
                errors.Add(nameof(updateDto.Ram), new List<string> { "RAM must be a positive number" });
                isValid = false;
            }

            // Validate Storage only if provided
            if (updateDto.Storage.HasValue && updateDto.Storage < 0)
            {
                errors.Add(nameof(updateDto.Storage), new List<string> { "Storage must be a positive number" });
                isValid = false;
            }

            // Validate StorageType only if provided
            if (!string.IsNullOrEmpty(updateDto.StorageType) && updateDto.StorageType.Length > 20)
            {
                errors.Add(nameof(updateDto.StorageType), new List<string> { "Storage type cannot exceed 20 characters" });
                isValid = false;
            }

            // Validate CurrentPrice only if provided
            if (updateDto.CurrentPrice.HasValue && updateDto.CurrentPrice < 0)
            {
                errors.Add(nameof(updateDto.CurrentPrice), new List<string> { "Current price must be a positive number" });
                isValid = false;
            }

            // Validate StockQuantity only if provided
            if (updateDto.StockQuantity.HasValue && updateDto.StockQuantity < 0)
            {
                errors.Add(nameof(updateDto.StockQuantity), new List<string> { "Stock quantity must be a positive number" });
                isValid = false;
            }

            // Validate ReorderLevel only if provided
            if (updateDto.ReorderLevel.HasValue && updateDto.ReorderLevel < 0)
            {
                errors.Add(nameof(updateDto.ReorderLevel), new List<string> { "Reorder level must be a positive number" });
                isValid = false;
            }

            return isValid;
        }

        private static bool IsAnyPropertyProvided(UpdateLaptopVariantDto updateDto)
        {
            return !string.IsNullOrEmpty(updateDto.Sku) ||
                   updateDto.Ram.HasValue ||
                   updateDto.Storage.HasValue ||
                   !string.IsNullOrEmpty(updateDto.StorageType) ||
                   updateDto.CurrentPrice.HasValue ||
                   updateDto.StockQuantity.HasValue ||
                   updateDto.ReorderLevel.HasValue ||
                   updateDto.IsActive.HasValue;
        }
    }
}