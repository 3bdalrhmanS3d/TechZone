using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Cart.RemoveFromCart.Commands;
using TechZoneV1.Features.Cart.RemoveFromCart.Dtos;
using TechZoneV1.Features.Cart.RemoveFromCart.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.RemoveFromCart.Endpoints
{
    public static class RemoveFromCartEndpoint
    {
        public static void MapRemoveFromCartEndpoint(this WebApplication app)
        {
            app.MapDelete("/api/cart/items/{itemId}", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromRoute] int itemId,
                IHttpContextAccessor httpContextAccessor) =>
            {
                // Validate itemId
                if (itemId <= 0)
                {
                    return EndpointResponse<RemoveFromCartViewModel>.ErrorResponse(
                        "Invalid cart item ID",
                        "معرف عنصر السلة غير صالح",
                        400
                    );
                }

                // Get current user ID
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return EndpointResponse<RemoveFromCartViewModel>.UnauthorizedResponse(
                        "Unauthorized access",
                        "الوصول غير مصرح به"
                    );
                }

                var command = new RemoveFromCartCommand(userId, itemId);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return EndpointResponse<RemoveFromCartViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    );
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return EndpointResponse<RemoveFromCartViewModel>.SuccessResponse(
                    data: viewModel,
                    message: result.Message,
                    messageAr: result.MessageAr
                );
            })
            .WithName("RemoveFromCart")
            .WithTags("Cart");
        }

        private static RemoveFromCartViewModel MapToViewModel(RemoveFromCartResponseDto dto)
        {
            return new RemoveFromCartViewModel
            {
                RemovedItemId = dto.RemovedItemId,
                CartSummary = new CartSummaryUpdateViewModel
                {
                    TotalItems = dto.CartSummary.TotalItems,
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