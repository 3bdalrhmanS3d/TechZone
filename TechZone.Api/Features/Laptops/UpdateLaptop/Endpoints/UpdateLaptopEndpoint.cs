using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TechZoneV1.Features.Laptops.UpdateLaptop.Commands;
using TechZoneV1.Features.Laptops.UpdateLaptop.Dtos;
using TechZoneV1.Features.Laptops.UpdateLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.UpdateLaptop.Endpoints
{
    public static class UpdateLaptopEndpoint
    {
        public static void MapUpdateLaptopEndpoint(this WebApplication app)
        {
            app.MapPut("/api/laptops/{id}", async (
               CancellationToken cancellationToken,
                IMediator mediator,
                [FromRoute] int id,
                [FromBody] UpdateMainLaptopDto updateLaptopDto) =>
            {
                // Validate ID
                if (id <= 0)
                {
                    return Results.BadRequest(EndpointResponse<MainLaptopUpdatedViewModel>.ErrorResponse(
                        "Invalid laptop ID",
                        "معرف اللابتوب غير صالح",
                        400
                    ));
                }

                // Validate the DTO
                if (!TryValidateUpdateLaptopDto(updateLaptopDto, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<MainLaptopUpdatedViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                var command = new UpdateLaptopCommand(id, updateLaptopDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<MainLaptopUpdatedViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                return Results.Ok(EndpointResponse<MainLaptopUpdatedViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("UpdateLaptop")
            .WithTags("Laptops");
        }

        private static bool TryValidateUpdateLaptopDto(UpdateMainLaptopDto dto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // ModelName validation (if provided)
            if (!string.IsNullOrEmpty(dto.ModelName) && dto.ModelName.Length > 200)
            {
                AddError(errors, nameof(dto.ModelName), "Model name cannot exceed 200 characters");
                isValid = false;
            }

            // BrandId validation (if provided)
            if (dto.BrandId.HasValue && dto.BrandId <= 0)
            {
                AddError(errors, nameof(dto.BrandId), "Brand ID must be greater than 0");
                isValid = false;
            }

            // CategoryId validation (if provided)
            if (dto.CategoryId.HasValue && dto.CategoryId <= 0)
            {
                AddError(errors, nameof(dto.CategoryId), "Category ID must be greater than 0");
                isValid = false;
            }

            // Processor validation (if provided)
            if (!string.IsNullOrEmpty(dto.Processor) && dto.Processor.Length > 100)
            {
                AddError(errors, nameof(dto.Processor), "Processor cannot exceed 100 characters");
                isValid = false;
            }

            // GPU validation (if provided)
            if (!string.IsNullOrEmpty(dto.GPU) && dto.GPU.Length > 100)
            {
                AddError(errors, nameof(dto.GPU), "GPU cannot exceed 100 characters");
                isValid = false;
            }

            // ScreenSize validation (if provided)
            if (!string.IsNullOrEmpty(dto.ScreenSize) && dto.ScreenSize.Length > 50)
            {
                AddError(errors, nameof(dto.ScreenSize), "Screen size cannot exceed 50 characters");
                isValid = false;
            }

            // StoreLocation validation (if provided)
            if (!string.IsNullOrEmpty(dto.StoreLocation) && dto.StoreLocation.Length > 200)
            {
                AddError(errors, nameof(dto.StoreLocation), "Store location cannot exceed 200 characters");
                isValid = false;
            }

            // StoreContact validation (if provided)
            if (!string.IsNullOrEmpty(dto.StoreContact) && dto.StoreContact.Length > 100)
            {
                AddError(errors, nameof(dto.StoreContact), "Store contact cannot exceed 100 characters");
                isValid = false;
            }

            // ReleaseYear validation (if provided)
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