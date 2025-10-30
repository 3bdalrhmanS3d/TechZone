using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Cart.ApplyDiscountCode.Commands;
using TechZoneV1.Features.Cart.ApplyDiscountCode.Dtos;
using TechZoneV1.Features.Cart.ApplyDiscountCode.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.ApplyDiscountCode.Endpoints
{
    public static class ApplyDiscountCodeEndpoint
    {
        public static void MapApplyDiscountCodeEndpoint(this WebApplication app)
        {
            app.MapPost("/api/cart/apply-discount", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromBody] ApplyDiscountCodeRequestDto requestDto,
                IHttpContextAccessor httpContextAccessor) =>
            {
                // Validate the DTO
                if (!TryValidateRequestDto(requestDto, out var validationErrors))
                {
                    return EndpointResponse<ApplyDiscountCodeViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    );
                }

                // Get current user ID
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return EndpointResponse<ApplyDiscountCodeViewModel>.UnauthorizedResponse(
                        "Unauthorized access",
                        "الوصول غير مصرح به"
                    );
                }

                var command = new ApplyDiscountCodeCommand(userId, requestDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return EndpointResponse<ApplyDiscountCodeViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    );
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return EndpointResponse<ApplyDiscountCodeViewModel>.SuccessResponse(
                    data: viewModel,
                    message: result.Message,
                    messageAr: result.MessageAr
                );
            })
            .WithName("ApplyDiscountCode")
            .WithTags("Cart");
        }

        private static bool TryValidateRequestDto(ApplyDiscountCodeRequestDto requestDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate Code
            if (string.IsNullOrWhiteSpace(requestDto.Code))
            {
                AddError(errors, nameof(requestDto.Code), "Discount code is required");
                isValid = false;
            }
            else if (requestDto.Code.Length > 50)
            {
                AddError(errors, nameof(requestDto.Code), "Discount code cannot exceed 50 characters");
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

        private static ApplyDiscountCodeViewModel MapToViewModel(ApplyDiscountCodeResponseDto dto)
        {
            return new ApplyDiscountCodeViewModel
            {
                Code = dto.Code,
                DiscountType = dto.DiscountType,
                Value = dto.Value,
                DiscountAmount = dto.DiscountAmount,
                CartSummary = new CartSummaryDiscountViewModel
                {
                    Subtotal = dto.CartSummary.Subtotal,
                    Discount = dto.CartSummary.Discount,
                    Total = dto.CartSummary.Total
                }
            };
        }

        private static string GetCurrentUserId(HttpContext? httpContext)
        {
            return httpContext?.User?.FindFirst("uid")?.Value ?? "";
        }
    }
}