using MediatR;
using TechZoneV1.Features.Cart.ClearCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.ClearCart.Commands
{
    public class ClearCartCommand : IRequest<RequestResponse<ClearCartResponseDto>>
    {
        public string UserId { get; }

        public ClearCartCommand(string userId)
        {
            UserId = userId;
        }
    }
}