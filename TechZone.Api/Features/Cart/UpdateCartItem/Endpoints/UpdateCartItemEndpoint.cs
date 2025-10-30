using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Cart.UpdateCartItem.Commands;
using TechZoneV1.Features.Cart.UpdateCartItem.Dtos;
using TechZoneV1.Features.Cart.UpdateCartItem.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.UpdateCartItem.Endpoints
{
    public static class UpdateCartItemEndpoint
    {
        public static void MapUpdateCartItemEndpoint(this WebApplication app)
        {
            app.MapPut("/api/cart/items/{itemId}", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromRoute] int itemId,
                [FromBody] UpdateCartItemRequestDto requestDto,
                IHttpContextAccessor httpContextAccessor) =>
            {
                // Validate the DTO
                if (!TryValidateRequestDto(requestDto, out var validationErrors))
                {
                    return EndpointResponse<UpdatedCartItemViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    );
                }

                // Validate itemId
                if (itemId <= 0)
                {
                    return EndpointResponse<UpdatedCartItemViewModel>.ErrorResponse(
                        "Invalid cart item ID",
                        "معرف عنصر السلة غير صالح",
                        400
                    );
                }

                // Get current user ID
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return EndpointResponse<UpdatedCartItemViewModel>.UnauthorizedResponse(
                        "Unauthorized access",
                        "الوصول غير مصرح به"
                    );
                }

                var command = new UpdateCartItemCommand(userId, itemId, requestDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return EndpointResponse<UpdatedCartItemViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    );
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return EndpointResponse<UpdatedCartItemViewModel>.SuccessResponse(
                    data: viewModel,
                    message: result.Message,
                    messageAr: result.MessageAr
                );
            })
            .WithName("UpdateCartItem")
            .WithTags("Cart");
        }

        private static bool TryValidateRequestDto(UpdateCartItemRequestDto requestDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate Quantity
            if (requestDto.Quantity <= 0)
            {
                AddError(errors, nameof(requestDto.Quantity), "Quantity must be at least 1");
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

        private static UpdatedCartItemViewModel MapToViewModel(UpdatedCartItemDto dto)
        {
            return new UpdatedCartItemViewModel
            {
                Id = dto.Id,
                ProductType = dto.ProductType,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TotalPrice = dto.TotalPrice,
                UpdatedAt = dto.UpdatedAt
            };
        }

        private static string GetCurrentUserId(HttpContext? httpContext)
        {
            // Implement your logic to get current user ID
            return httpContext?.User?.FindFirst("uid")?.Value ?? "";
        }
    }
}