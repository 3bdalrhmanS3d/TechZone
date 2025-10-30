using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZone.Domain.Entities;
using TechZoneV1.Features.Cart.AddToCart.Commands;
using TechZoneV1.Features.Cart.AddToCart.Dtos;
using TechZoneV1.Features.Cart.AddToCart.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.AddToCart.Endpoints
{
    public static class AddToCartEndpoint
    {
        public static void MapAddToCartEndpoint(this WebApplication app)
        {
            app.MapPost("/api/cart/items", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromBody] AddToCartRequestDto requestDto,
                IHttpContextAccessor httpContextAccessor) =>
            {
                // Validate the DTO
                if (!TryValidateRequestDto(requestDto, out var validationErrors))
                {
                    return EndpointResponse<AddedCartItemViewModel>.ValidationErrorResponse(
                        validationErrors,
                        "Validation failed",
                        "فشل التحقق من الصحة"
                    );
                }

                // Get current user ID
                var userId = GetCurrentUserId(httpContextAccessor.HttpContext);
                if (string.IsNullOrEmpty(userId))
                {
                    return EndpointResponse<AddedCartItemViewModel>.UnauthorizedResponse(
                        "Unauthorized access",
                        "الوصول غير مصرح به"
                    );
                }

                var command = new AddToCartCommand(userId, requestDto);
                var result = await mediator.Send(command, cancellationToken);

                if (!result.IsSuccess)
                {
                    return EndpointResponse<AddedCartItemViewModel>.ErrorResponse(
                        result.Message,
                        result.MessageAr,
                        400
                    );
                }

                // Map DTO to ViewModel in the endpoint
                var viewModel = MapToViewModel(result.Data!);

                return EndpointResponse<AddedCartItemViewModel>.SuccessResponse(
                        data: viewModel,
                        message: result.Message,
                        messageAr: result.MessageAr,
                        statusCode: 201
                );
            })
            .WithName("AddToCart")
            .WithTags("Cart");
        }

        private static bool TryValidateRequestDto(AddToCartRequestDto requestDto, out Dictionary<string, List<string>> errors)
        {
            errors = new Dictionary<string, List<string>>();
            var isValid = true;

            // Validate ProductType
            if (!Enum.IsDefined(typeof(ProductType), requestDto.ProductType))
            {
                AddError(errors, nameof(requestDto.ProductType), "Invalid product type");
                isValid = false;
            }

            // Validate ProductId
            if (requestDto.ProductId <= 0)
            {
                AddError(errors, nameof(requestDto.ProductId), "Product ID must be greater than 0");
                isValid = false;
            }

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

        private static AddedCartItemViewModel MapToViewModel(AddedCartItemDto dto)
        {
            return new AddedCartItemViewModel
            {
                Id = dto.Id,
                ProductType = dto.ProductType,
                ProductId = dto.ProductId,
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TotalPrice = dto.TotalPrice,
                AddedAt = dto.AddedAt,
                CartSummary = new CartSummaryViewModel
                {
                    TotalItems = dto.CartSummary.TotalItems,
                    Total = dto.CartSummary.Total
                }
            };
        }

        private static string GetCurrentUserId(HttpContext? httpContext)
        {
            // Implement your logic to get current user ID
            return httpContext?.User?.FindFirst("uid")?.Value ?? "";
        }
    }
}