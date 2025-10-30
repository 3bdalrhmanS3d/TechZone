using MediatR;
using TechZoneV1.Features.Cart.ApplyDiscountCode.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.ApplyDiscountCode.Commands
{
    public class ApplyDiscountCodeCommand : IRequest<RequestResponse<ApplyDiscountCodeResponseDto>>
    {
        public string UserId { get; }
        public ApplyDiscountCodeRequestDto RequestDto { get; }

        public ApplyDiscountCodeCommand(string userId, ApplyDiscountCodeRequestDto requestDto)
        {
            UserId = userId;
            RequestDto = requestDto;
        }
    }
}