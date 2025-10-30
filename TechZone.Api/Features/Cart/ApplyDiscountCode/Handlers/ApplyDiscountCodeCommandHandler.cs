using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.ApplyDiscountCode.Commands;
using TechZoneV1.Features.Cart.ApplyDiscountCode.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.ApplyDiscountCode.Handlers
{
    public class ApplyDiscountCodeCommandHandler : IRequestHandler<ApplyDiscountCodeCommand, RequestResponse<ApplyDiscountCodeResponseDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<Discount> _discountRepository;
        private readonly IBaseRepository<ProductDiscount> _productDiscountRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<UserDiscountUsage> _userDiscountUsageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApplyDiscountCodeCommandHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<Discount> discountRepository,
            IBaseRepository<ProductDiscount> productDiscountRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<UserDiscountUsage> userDiscountUsageRepository,
            IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _discountRepository = discountRepository;
            _productDiscountRepository = productDiscountRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _userDiscountUsageRepository = userDiscountUsageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<ApplyDiscountCodeResponseDto>> Handle(
            ApplyDiscountCodeCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var requestDto = request.RequestDto;
                var currentDate = DateTime.UtcNow;

                // Get the discount by code
                var discount = await _discountRepository.GetAll()
                    .FirstOrDefaultAsync(d => d.Code == requestDto.Code && d.IsActive, cancellationToken);

                if (discount == null)
                {
                    return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                        "Invalid discount code",
                        "رمز الخصم غير صالح"
                    );
                }

                // Check date range
                if (currentDate < discount.StartDate || currentDate > discount.EndDate)
                {
                    return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                        "Discount code is expired or not yet active",
                        "رمز الخصم منتهي الصلاحية أو غير نشط بعد"
                    );
                }

                // Check usage limit
                if (discount.UsageLimit.HasValue && discount.UsageCount >= discount.UsageLimit.Value)
                {
                    return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                        "Discount code has reached its usage limit",
                        "تم الوصول إلى الحد الأقصى لاستخدام رمز الخصم"
                    );
                }

                // Check if user has already used this discount
                var userUsage = await _userDiscountUsageRepository.GetAll()
                    .FirstOrDefaultAsync(udu => udu.UserId == request.UserId && udu.DiscountId == discount.Id, cancellationToken);

                if (userUsage != null && discount.UsageLimit.HasValue)
                {
                    return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                        "You have already used this discount code",
                        "لقد استخدمت رمز الخصم هذا مسبقاً"
                    );
                }

                // Get user's cart items
                var cartItems = await _cartItemRepository.GetAll()
                    .Where(ci => ci.UserId == request.UserId && !ci.IsDeleted)
                    .ToListAsync(cancellationToken);

                if (!cartItems.Any())
                {
                    return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                        "Cart is empty",
                        "السلة فارغة"
                    );
                }

                // Calculate cart subtotal
                var (subtotal, eligibleSubtotal) = await CalculateCartSubtotals(cartItems, discount.Id, cancellationToken);

                // Check minimum purchase requirement
                if (subtotal < discount.MinimumPurchase)
                {
                    return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                        $"Minimum purchase of {discount.MinimumPurchase} is required for this discount",
                        $"الحد الأدنى للشراء هو {discount.MinimumPurchase} لاستخدام هذا الخصم"
                    );
                }

                // Calculate discount amount
                var discountAmount = CalculateDiscountAmount(discount, eligibleSubtotal);

                // Record discount usage
                if (userUsage == null)
                {
                    var newUsage = new UserDiscountUsage
                    {
                        UserId = request.UserId,
                        DiscountId = discount.Id,
                        UsedAt = DateTime.UtcNow
                    };
                    await _userDiscountUsageRepository.AddAsync(newUsage);

                    // Update discount usage count
                    discount.UsageCount++;
                    _discountRepository.Update(discount);

                    await _unitOfWork.SaveChangesAsync();
                }

                // Return DTO
                var responseDto = new ApplyDiscountCodeResponseDto
                {
                    Code = discount.Code,
                    DiscountType = discount.DiscountType,
                    Value = discount.Value,
                    DiscountAmount = discountAmount,
                    CartSummary = new CartSummaryDiscountDto
                    {
                        Subtotal = subtotal,
                        Discount = discountAmount,
                        Total = subtotal - discountAmount
                    }
                };

                return RequestResponse<ApplyDiscountCodeResponseDto>.Success(
                    responseDto,
                    "Discount code applied successfully",
                    "تم تطبيق رمز الخصم بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<ApplyDiscountCodeResponseDto>.Fail(
                    "An error occurred while applying discount code",
                    "حدث خطأ أثناء تطبيق رمز الخصم"
                );
            }
        }

        private async Task<(decimal subtotal, decimal eligibleSubtotal)> CalculateCartSubtotals(
            List<CartItem> cartItems, int discountId, CancellationToken cancellationToken)
        {
            decimal subtotal = 0;
            decimal eligibleSubtotal = 0;

            // Get product discounts for this discount
            var productDiscounts = await _productDiscountRepository.GetAll()
                .Where(pd => pd.DiscountId == discountId)
                .ToListAsync(cancellationToken);

            // Get all laptop variant prices
            var laptopVariantIds = cartItems
                .Where(ci => ci.ProductType == ProductType.LaptopVariant)
                .Select(ci => ci.ProductId)
                .Distinct()
                .ToList();

            var laptopVariants = await _laptopVariantRepository.GetAll()
                .Where(lv => laptopVariantIds.Contains(lv.Id))
                .ToDictionaryAsync(lv => lv.Id, lv => lv.CurrentPrice, cancellationToken);

            foreach (var cartItem in cartItems)
            {
                decimal unitPrice = 0;

                if (cartItem.ProductType == ProductType.LaptopVariant)
                {
                    if (laptopVariants.TryGetValue(cartItem.ProductId, out var price))
                    {
                        unitPrice = price;
                    }
                }
                // Add other product types as needed

                var itemTotal = unitPrice * cartItem.Quantity;
                subtotal += itemTotal;

                // Check if this product is eligible for the discount
                var isEligible = !productDiscounts.Any() || // If no product discounts, all products are eligible
                    productDiscounts.Any(pd => pd.ProductType == cartItem.ProductType && pd.ProductId == cartItem.ProductId);

                if (isEligible)
                {
                    eligibleSubtotal += itemTotal;
                }
            }

            return (subtotal, eligibleSubtotal);
        }

        private decimal CalculateDiscountAmount(Discount discount, decimal eligibleSubtotal)
        {
            decimal discountAmount = 0;

            if (discount.DiscountType == DiscountType.Percentage)
            {
                discountAmount = eligibleSubtotal * (discount.Value / 100m);
                if (discount.MaxDiscountAmount > 0 && discountAmount > discount.MaxDiscountAmount)
                {
                    discountAmount = discount.MaxDiscountAmount;
                }
            }
            else // FixedAmount
            {
                discountAmount = discount.Value;
                // Cap at eligible subtotal to avoid negative total
                if (discountAmount > eligibleSubtotal)
                {
                    discountAmount = eligibleSubtotal;
                }
            }

            return discountAmount;
        }
    }
}