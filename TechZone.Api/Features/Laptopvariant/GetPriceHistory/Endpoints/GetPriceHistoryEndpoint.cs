using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.Dtos;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.Queries;
using TechZoneV1.Features.LaptopVariant.GetPriceHistory.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetPriceHistory.Endpoints
{
    public static class GetPriceHistoryEndpoint
    {
        public static void MapGetPriceHistoryEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptop-variants/{id}/price-history", async (
                CancellationToken cancellationToken,
                IMediator mediator,
                [FromRoute] int id,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10,
                [FromQuery] int? days = null) =>
            {
                // Validate ID
                if (id <= 0)
                {
                    return Results.BadRequest(EndpointResponse<PriceHistoryResponseViewModel>.ErrorResponse(
                        "Invalid laptop variant ID",
                        "معرف نوع اللابتوب غير صالح",
                        400
                    ));
                }

                // Validate query parameters
                if (!TryValidateQueryParameters(page, pageSize, days, out var validationErrors))
                {
                    return Results.BadRequest(EndpointResponse<PriceHistoryResponseViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    ));
                }

                var queryDto = new PriceHistoryQueryDto
                {
                    Page = page,
                    PageSize = pageSize,
                    Days = days
                };

                var query = new GetPriceHistoryQuery(id, queryDto);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.NotFound(EndpointResponse<PriceHistoryResponseViewModel>.NotFoundResponse(
                        result.Message,
                        result.MessageAr
                    ));
                }

                return Results.Ok(EndpointResponse<PriceHistoryResponseViewModel>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetPriceHistory")
            .WithTags("Laptop Variants");
        }

        private static bool TryValidateQueryParameters(int page, int pageSize, int? days, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate page
            if (page < 1)
            {
                errors.Add(nameof(page), new List<string> { "Page must be greater than 0" });
                isValid = false;
            }

            // Validate pageSize
            if (pageSize < 1 || pageSize > 100)
            {
                errors.Add(nameof(pageSize), new List<string> { "PageSize must be between 1 and 100" });
                isValid = false;
            }

            // Validate days
            if (days.HasValue && (days < 1 || days > 3650))
            {
                errors.Add(nameof(days), new List<string> { "Days must be between 1 and 3650" });
                isValid = false;
            }

            return isValid;
        }
    }
}