using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Laptops.CreateLaptop.Commands;
using TechZoneV1.Features.Laptops.CreateLaptop.Dtos;
using TechZoneV1.Features.Laptops.CreateLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.CreateLaptop.Endpoints
{
    public static class CreateLaptopEndpoint
    {
        public static void MapCreateLaptopEndpoint(this WebApplication app)
        {
            app.MapPost("/api/laptops", async (
                IMediator mediator,
                [FromBody] CreateFullLaptopDto createFullLaptopDto,
                CancellationToken cancellationToken) =>
            {
                // Validate the DTO
                if (!TryValidateCreateLaptopDto(createFullLaptopDto, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<LaptopCreatedViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                var command = new CreateLaptopCommand(createFullLaptopDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<LaptopCreatedViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Created(
                    $"/api/laptops/{result.Data!.Id}",
                    EndpointResponse<LaptopCreatedViewModel>.SuccessResponse(
                        data: result.Data!,
                        message: result.Message,
                        messageAr: result.MessageAr,
                        statusCode: 201
                    )
                );
            })
            .WithName("CreateLaptop")
            .WithTags("Laptops");
        }

        private static bool TryValidateCreateLaptopDto(CreateFullLaptopDto dto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // ModelName validation
            if (string.IsNullOrWhiteSpace(dto.ModelName))
            {
                AddError(errors, nameof(dto.ModelName), "Model name is required");
                isValid = false;
            }
            else if (dto.ModelName.Length > 200)
            {
                AddError(errors, nameof(dto.ModelName), "Model name cannot exceed 200 characters");
                isValid = false;
            }

            // BrandId validation
            if (dto.BrandId <= 0)
            {
                AddError(errors, nameof(dto.BrandId), "Brand ID must be greater than 0");
                isValid = false;
            }

            // CategoryId validation
            if (dto.CategoryId <= 0)
            {
                AddError(errors, nameof(dto.CategoryId), "Category ID must be greater than 0");
                isValid = false;
            }

            // Processor validation
            if (string.IsNullOrWhiteSpace(dto.Processor))
            {
                AddError(errors, nameof(dto.Processor), "Processor is required");
                isValid = false;
            }
            else if (dto.Processor.Length > 100)
            {
                AddError(errors, nameof(dto.Processor), "Processor cannot exceed 100 characters");
                isValid = false;
            }

            // GPU validation
            if (dto.GPU?.Length > 100)
            {
                AddError(errors, nameof(dto.GPU), "GPU cannot exceed 100 characters");
                isValid = false;
            }

            // ScreenSize validation
            if (dto.ScreenSize?.Length > 50)
            {
                AddError(errors, nameof(dto.ScreenSize), "Screen size cannot exceed 50 characters");
                isValid = false;
            }

            // StoreLocation validation
            if (dto.StoreLocation?.Length > 200)
            {
                AddError(errors, nameof(dto.StoreLocation), "Store location cannot exceed 200 characters");
                isValid = false;
            }

            // StoreContact validation
            if (dto.StoreContact?.Length > 100)
            {
                AddError(errors, nameof(dto.StoreContact), "Store contact cannot exceed 100 characters");
                isValid = false;
            }

            // ReleaseYear validation
            if (dto.ReleaseYear.HasValue && (dto.ReleaseYear < 2000 || dto.ReleaseYear > 2030))
            {
                AddError(errors, nameof(dto.ReleaseYear), "Release year must be between 2000 and 2030");
                isValid = false;
            }

            return isValid;
        }

        private static void AddError(Dictionary<string, List<string>> errors, string field, string message)
        {
            if (!errors.ContainsKey(field))
            {
                errors[field] = new List<string>();
            }
            errors[field].Add(message);
        }
    }
}