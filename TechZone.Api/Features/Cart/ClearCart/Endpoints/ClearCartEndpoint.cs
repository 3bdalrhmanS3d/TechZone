using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Cart.ClearCart.Commands;
using TechZoneV1.Features.Cart.ClearCart.Dtos;
using TechZoneV1.Features.Cart.ClearCart.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.ClearCart.Endpoints
{
    public static class ClearCartEndpoint
    {
        public static void MapClearCartEndpoint(this WebApplication app)
        {
            app.MapDelete("/api/cart", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                IHttpContextAccessor httpContextAccessor) =>
            {
                // Get current user ID
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return EndpointResponse<ClearCartViewModel>.UnauthorizedResponse(
                        "Unauthorized access",
                        "الوصول غير مصرح به"
                    );
                }

                var command = new ClearCartCommand(userId);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return EndpointResponse<ClearCartViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    );
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return EndpointResponse<ClearCartViewModel>.SuccessResponse(
                    data: viewModel,
                    message: result.Message,
                    messageAr: result.MessageAr
                );
            })
            .WithName("ClearCart")
            .WithTags("Cart");
        }

        private static ClearCartViewModel MapToViewModel(ClearCartResponseDto dto)
        {
            return new ClearCartViewModel
            {
                ItemsRemoved = dto.ItemsRemoved,
                ClearedAt = dto.ClearedAt
            };
        }

        private static string GetCurrentUserId(HttpContext? httpContext)
        {
            return httpContext?.User?.FindFirst("uid")?.Value ?? "";
        }
    }
}