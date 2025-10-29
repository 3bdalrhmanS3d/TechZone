using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZoneV1.Features.Cart.GetCart.Dtos;
using TechZoneV1.Features.Cart.GetCart.Queries;
using TechZoneV1.Features.Cart.GetCart.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.GetCart.Endpoints
{
    public static class GetCartEndpoint
    {
        public static void MapGetCartEndpoint(this WebApplication app)
        {
            app.MapGet("/api/cart", async (
                IMediator mediator,
                [FromServices] IHttpContextAccessor httpContextAccessor,
                CancellationToken cancellationToken) =>
            {
                // Get current user ID (you'll need to implement your user context)
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }

                var requestDto = new GetCartRequestDto();
                var query = new GetCartQuery(userId, requestDto);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.BadRequest(EndpointResponse<CartViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    ));
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return Results.Ok(EndpointResponse<CartViewModel>.SuccessResponse(
                    data: viewModel,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetCart")
            .WithTags("Cart")
            .Produces<EndpointResponse<CartViewModel>>(StatusCodes.Status200OK)
            .Produces<EndpointResponse<CartViewModel>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }

        private static CartViewModel MapToViewModel(CartDto cartDto)
        {
            return new CartViewModel
            {
                Items = cartDto.Items.Select(item => new CartItemViewModel
                {
                    Id = item.Id,
                    ProductType = item.ProductType,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    SKU = item.SKU,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    DiscountAmount = item.DiscountAmount,
                    TotalPrice = item.TotalPrice,
                    StockAvailable = item.StockAvailable,
                    Image = item.Image,
                    AddedAt = item.AddedAt
                }).ToList(),
                TotalItems = cartDto.TotalItems,
                Subtotal = cartDto.Subtotal,
                Discount = cartDto.Discount,
                Tax = cartDto.Tax,
                Shipping = cartDto.Shipping,
                Total = cartDto.Total,
                AppliedDiscountCode = cartDto.AppliedDiscountCode
            };
        }

        private static string GetCurrentUserId(HttpContext? httpContext)
        {
            // Implement your logic to get current user ID
            // This is just a placeholder - you'll need to integrate with your authentication system
            return httpContext?.User?.FindFirst("uid")?.Value ?? "current-user-id";
        }
    }
}