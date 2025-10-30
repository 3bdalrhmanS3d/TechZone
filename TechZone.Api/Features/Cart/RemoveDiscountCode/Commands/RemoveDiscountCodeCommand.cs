using MediatR;
using TechZoneV1.Features.Cart.RemoveDiscountCode.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.RemoveDiscountCode.Commands
{
    public class RemoveDiscountCodeCommand : IRequest<RequestResponse<RemoveDiscountCodeResponseDto>>
    {
        public string UserId { get; }

        public RemoveDiscountCodeCommand(string userId)
        {
            UserId = userId;
        }
    }
}