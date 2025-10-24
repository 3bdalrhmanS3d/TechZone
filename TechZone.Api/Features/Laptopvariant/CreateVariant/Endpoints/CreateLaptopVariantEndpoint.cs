using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.LaptopVariant.CreateVariant.Commands;
using TechZoneV1.Features.LaptopVariant.CreateVariant.Dtos;
using TechZoneV1.Features.LaptopVariant.CreateVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.CreateVariant.Endpoints
{
    public static class CreateLaptopVariantEndpoint
    {
        public static void MapCreateLaptopVariantEndpoint(this WebApplication app)
        {
            app.MapPost("/api/laptop-variants", async (
                IMediator mediator,
                [FromBody] CreateLaptopVariantDto createDto,
                CancellationToken cancellationToken) =>
            {
                // Validate the DTO
                if (!TryValidateCreateDto(createDto, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<LaptopVariantViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                var command = new CreateLaptopVariantCommand(createDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<LaptopVariantViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Created($"/api/laptop-variants/{result.Data!.Id}",
                    EndpointResponse<LaptopVariantViewModel>.SuccessResponse(
                        data: result.Data!,
                        message: result.Message,
                        messageAr: result.MessageAr,
                        statusCode: 201
                    ));
            })
            .WithName("CreateLaptopVariant")
            .WithTags("Laptop Variants");
        }

        private static bool TryValidateCreateDto(CreateLaptopVariantDto createDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate LaptopId
            if (createDto.LaptopId <= 0)
            {
                errors.Add(nameof(createDto.LaptopId), new List<string> { "Laptop ID must be greater than 0" });
                isValid = false;
            }

            // Validate SKU
            if (string.IsNullOrWhiteSpace(createDto.Sku))
            {
                errors.Add(nameof(createDto.Sku), new List<string> { "SKU is required" });
                isValid = false;
            }
            else if (createDto.Sku.Length > 100)
            {
                errors.Add(nameof(createDto.Sku), new List<string> { "SKU cannot exceed 100 characters" });
                isValid = false;
            }

            // Validate RAM
            if (createDto.Ram < 0)
            {
                errors.Add(nameof(createDto.Ram), new List<string> { "RAM must be a positive number" });
                isValid = false;
            }

            // Validate Storage
            if (createDto.Storage < 0)
            {
                errors.Add(nameof(createDto.Storage), new List<string> { "Storage must be a positive number" });
                isValid = false;
            }

            // Validate StorageType
            if (string.IsNullOrWhiteSpace(createDto.StorageType))
            {
                errors.Add(nameof(createDto.StorageType), new List<string> { "Storage type is required" });
                isValid = false;
            }
            else if (createDto.StorageType.Length > 20)
            {
                errors.Add(nameof(createDto.StorageType), new List<string> { "Storage type cannot exceed 20 characters" });
                isValid = false;
            }

            // Validate CurrentPrice
            if (createDto.CurrentPrice < 0)
            {
                errors.Add(nameof(createDto.CurrentPrice), new List<string> { "Current price must be a positive number" });
                isValid = false;
            }

            // Validate StockQuantity
            if (createDto.StockQuantity < 0)
            {
                errors.Add(nameof(createDto.StockQuantity), new List<string> { "Stock quantity must be a positive number" });
                isValid = false;
            }

            // Validate ReorderLevel
            if (createDto.ReorderLevel < 0)
            {
                errors.Add(nameof(createDto.ReorderLevel), new List<string> { "Reorder level must be a positive number" });
                isValid = false;
            }

            return isValid;
        }
    }
}