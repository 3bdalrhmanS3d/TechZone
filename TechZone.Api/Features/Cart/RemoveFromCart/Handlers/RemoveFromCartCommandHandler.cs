using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.RemoveFromCart.Commands;
using TechZoneV1.Features.Cart.RemoveFromCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.RemoveFromCart.Handlers
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, RequestResponse<RemoveFromCartResponseDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveFromCartCommandHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<RemoveFromCartResponseDto>> Handle(
            RemoveFromCartCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get the cart item with user validation
                var cartItem = await _cartItemRepository.GetAll()
                    .Where(ci => ci.Id == request.ItemId &&
                                 ci.UserId == request.UserId &&
                                 !ci.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (cartItem == null)
                {
                    return RequestResponse<RemoveFromCartResponseDto>.Fail(
                        "Cart item not found",
                        "عنصر السلة غير موجود"
                    );
                }

                // Soft delete the cart item
                _cartItemRepository.HardDelete(cartItem);
                await _unitOfWork.SaveChangesAsync();

                // Get updated cart summary
                var cartSummary = await GetCartSummary(request.UserId, cancellationToken);

                // Return DTO
                var responseDto = new RemoveFromCartResponseDto
                {
                    RemovedItemId = request.ItemId,
                    CartSummary = cartSummary
                };

                return RequestResponse<RemoveFromCartResponseDto>.Success(
                    responseDto,
                    "Item removed from cart successfully",
                    "تم إزالة العنصر من السلة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<RemoveFromCartResponseDto>.Fail(
                    "An error occurred while removing item from cart",
                    "حدث خطأ أثناء إزالة العنصر من السلة"
                );
            }
        }

        private async Task<CartSummaryRemoveDto> GetCartSummary(string userId, CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemRepository.GetAll()
                .Where(ci => ci.UserId == userId && !ci.IsDeleted)
                .ToListAsync(cancellationToken);

            if (!cartItems.Any())
            {
                return new CartSummaryRemoveDto { TotalItems = 0, Total = 0 };
            }

            var totalItems = cartItems.Sum(ci => ci.Quantity);
            decimal total = 0;

            var laptopVariantIds = cartItems
                .Where(ci => ci.ProductType == ProductType.LaptopVariant)
                .Select(ci => ci.ProductId)
                .Distinct()
                .ToList();

            if (laptopVariantIds.Any())
            {
                var laptopVariants = await _laptopVariantRepository.GetAll()
                    .Where(lv => laptopVariantIds.Contains(lv.Id))
                    .ToDictionaryAsync(lv => lv.Id, lv => lv.CurrentPrice, cancellationToken);

                foreach (var cartItem in cartItems.Where(ci => ci.ProductType == ProductType.LaptopVariant))
                {
                    if (laptopVariants.TryGetValue(cartItem.ProductId, out var price))
                    {
                        total += price * cartItem.Quantity;
                    }
                }
            }

            return new CartSummaryRemoveDto
            {
                TotalItems = totalItems,
                Total = total
            };
        }
    }
}