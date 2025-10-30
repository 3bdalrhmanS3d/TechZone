using MediatR;
using TechZoneV1.Features.Cart.UpdateCartItem.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.UpdateCartItem.Commands
{
    public class UpdateCartItemCommand : IRequest<RequestResponse<UpdatedCartItemDto>>
    {
        public string UserId { get; }
        public int ItemId { get; }
        public UpdateCartItemRequestDto RequestDto { get; }

        public UpdateCartItemCommand(string userId, int itemId, UpdateCartItemRequestDto requestDto)
        {
            UserId = userId;
            ItemId = itemId;
            RequestDto = requestDto;
        }
    }
}