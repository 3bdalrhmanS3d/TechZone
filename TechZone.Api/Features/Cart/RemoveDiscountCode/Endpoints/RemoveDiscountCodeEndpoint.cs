using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Cart.AddToCart.ViewModels;
using TechZoneV1.Features.Cart.RemoveDiscountCode.Commands;
using TechZoneV1.Features.Cart.RemoveDiscountCode.Dtos;
using TechZoneV1.Features.Cart.RemoveDiscountCode.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.RemoveDiscountCode.Endpoints
{
    public static class RemoveDiscountCodeEndpoint
    {
        public static void MapRemoveDiscountCodeEndpoint(this WebApplication app)
        {
            app.MapDelete("/api/cart/discount", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                IHttpContextAccessor httpContextAccessor) =>
            {
                // Get current user ID
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return EndpointResponse<RemoveDiscountCodeViewModel>.UnauthorizedResponse(
                        "Unauthorized access",
                        "الوصول غير مصرح به"
                    );
                }

                var command = new RemoveDiscountCodeCommand(userId);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return EndpointResponse<RemoveDiscountCodeViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    );
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return EndpointResponse<RemoveDiscountCodeViewModel>.SuccessResponse(
                    data: viewModel,
                    message: result.Message,
                    messageAr: result.MessageAr
                );
            })
            .WithName("RemoveDiscountCode")
            .WithTags("Cart");
        }

        private static RemoveDiscountCodeViewModel MapToViewModel(RemoveDiscountCodeResponseDto dto)
        {
            return new RemoveDiscountCodeViewModel
            {
                CartSummary = new CartSummaryRemoveDiscountViewModel
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