using MediatR;
using TechZoneV1.Features.Cart.GetCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.GetCart.Queries
{
    public class GetCartQuery : IRequest<RequestResponse<CartDto>>
    {
        public string UserId { get; }
        public GetCartRequestDto RequestDto { get; }

        public GetCartQuery(string userId, GetCartRequestDto requestDto)
        {
            UserId = userId;
            RequestDto = requestDto;
        }
    }
}