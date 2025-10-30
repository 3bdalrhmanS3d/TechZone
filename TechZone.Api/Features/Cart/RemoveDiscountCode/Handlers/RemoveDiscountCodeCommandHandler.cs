using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.RemoveDiscountCode.Commands;
using TechZoneV1.Features.Cart.RemoveDiscountCode.Dtos;
using TechZoneV1.Features.Cart.RemoveDiscountCode.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.RemoveDiscountCode.Handlers
{
    public class RemoveDiscountCodeCommandHandler : IRequestHandler<RemoveDiscountCodeCommand, RequestResponse<RemoveDiscountCodeResponseDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<UserDiscountUsage> _userDiscountUsageRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveDiscountCodeCommandHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<UserDiscountUsage> userDiscountUsageRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _userDiscountUsageRepository = userDiscountUsageRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<RemoveDiscountCodeResponseDto>> Handle(
            RemoveDiscountCodeCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get user's discount usage
                var userDiscountUsage = await _userDiscountUsageRepository.GetAll()
                    .Where(udu => udu.UserId == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (userDiscountUsage == null)
                {
                    return RequestResponse<RemoveDiscountCodeResponseDto>.Success(
                        new RemoveDiscountCodeResponseDto
                        {
                            CartSummary = await GetCartSummary(request.UserId)
                        },
                        "No discount code applied",
                        "لا يوجد رمز خصم مطبق"
                    );
                }

                // Remove the discount usage
                _userDiscountUsageRepository.Delete(userDiscountUsage);
                await _unitOfWork.SaveChangesAsync();

                // Get updated cart summary
                var cartSummary = await GetCartSummary(request.UserId);

                // Return DTO
                var responseDto = new RemoveDiscountCodeResponseDto
                {
                    CartSummary = cartSummary
                };

                return RequestResponse<RemoveDiscountCodeResponseDto>.Success(
                    responseDto,
                    "Discount code removed successfully",
                    "تم إزالة رمز الخصم بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<RemoveDiscountCodeResponseDto>.Fail(
                    "An error occurred while removing discount code",
                    "حدث خطأ أثناء إزالة رمز الخصم"
                );
            }
        }

        private async Task<CartSummaryRemoveDiscountDto> GetCartSummary(string userId)
        {
            var cartItems = await _cartItemRepository.GetAll()
                .Where(ci => ci.UserId == userId && !ci.IsDeleted)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return new CartSummaryRemoveDiscountDto { Subtotal = 0, Discount = 0, Total = 0 };
            }

            decimal subtotal = 0;

            var laptopVariantIds = cartItems
                .Where(ci => ci.ProductType == ProductType.LaptopVariant)
                .Select(ci => ci.ProductId)
                .Distinct()
                .ToList();

            if (laptopVariantIds.Any())
            {
                var laptopVariants = await _laptopVariantRepository.GetAll()
                    .Where(lv => laptopVariantIds.Contains(lv.Id))
                    .ToDictionaryAsync(lv => lv.Id, lv => lv.CurrentPrice);

                foreach (var cartItem in cartItems.Where(ci => ci.ProductType == ProductType.LaptopVariant))
                {
                    if (laptopVariants.TryGetValue(cartItem.ProductId, out var price))
                    {
                        subtotal += price * cartItem.Quantity;
                    }
                }
            }

            return new CartSummaryRemoveDiscountDto
            {
                Subtotal = subtotal,
                Discount = 0, // Since we removed the discount
                Total = subtotal
            };
        }
    }
}