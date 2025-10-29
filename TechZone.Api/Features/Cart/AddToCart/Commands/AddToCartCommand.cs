using MediatR;
using TechZoneV1.Features.Cart.AddToCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.AddToCart.Commands
{
    public class AddToCartCommand : IRequest<RequestResponse<AddedCartItemDto>>
    {
        public string UserId { get; }
        public AddToCartRequestDto RequestDto { get; }

        public AddToCartCommand(string userId, AddToCartRequestDto requestDto)
        {
            UserId = userId;
            RequestDto = requestDto;
        }
    }
}