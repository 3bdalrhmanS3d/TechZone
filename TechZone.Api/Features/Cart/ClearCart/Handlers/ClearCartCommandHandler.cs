using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.ClearCart.Commands;
using TechZoneV1.Features.Cart.ClearCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.ClearCart.Handlers
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, RequestResponse<ClearCartResponseDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClearCartCommandHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<ClearCartResponseDto>> Handle(
            ClearCartCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get all cart items for the user
                var cartItems = await _cartItemRepository.GetAll()
                    .Where(ci => ci.UserId == request.UserId && !ci.IsDeleted)
                    .ToListAsync(cancellationToken);

                if (!cartItems.Any())
                {
                    return RequestResponse<ClearCartResponseDto>.Success(
                        new ClearCartResponseDto { ItemsRemoved = 0, ClearedAt = DateTime.UtcNow },
                        "Cart is already empty",
                        "السلة فارغة بالفعل"
                    );
                }

                var itemsCount = cartItems.Count;

                // Soft delete all cart items
                foreach (var cartItem in cartItems)
                {
                    _cartItemRepository.HardDelete(cartItem);
                }

                await _unitOfWork.SaveChangesAsync();

                // Return DTO
                var responseDto = new ClearCartResponseDto
                {
                    ItemsRemoved = itemsCount,
                    ClearedAt = DateTime.UtcNow
                };

                return RequestResponse<ClearCartResponseDto>.Success(
                    responseDto,
                    "Cart cleared successfully",
                    "تم مسح السلة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<ClearCartResponseDto>.Fail(
                    "An error occurred while clearing cart",
                    "حدث خطأ أثناء مسح السلة"
                );
            }
        }
    }
}