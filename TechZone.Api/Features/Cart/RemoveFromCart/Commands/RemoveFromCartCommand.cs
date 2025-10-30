using MediatR;
using TechZoneV1.Features.Cart.RemoveFromCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.RemoveFromCart.Commands
{
    public class RemoveFromCartCommand : IRequest<RequestResponse<RemoveFromCartResponseDto>>
    {
        public string UserId { get; }
        public int ItemId { get; }

        public RemoveFromCartCommand(string userId, int itemId)
        {
            UserId = userId;
            ItemId = itemId;
        }
    }
}