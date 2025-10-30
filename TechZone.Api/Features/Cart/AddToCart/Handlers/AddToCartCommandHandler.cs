using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.AddToCart.Commands;
using TechZoneV1.Features.Cart.AddToCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.AddToCart.Handlers
{
    public class AddToCartCommandHandler
        : IRequestHandler<AddToCartCommand, RequestResponse<AddedCartItemDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddToCartCommandHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<AddedCartItemDto>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var dto = request.RequestDto;

                // ✅ Validate Product
                var (validateResult, productName, unitPrice, availableStock) =
                    await ValidateProduct(dto.ProductType, dto.ProductId, cancellationToken);

                if (!validateResult.IsSuccess)
                    return validateResult;

                // ✅ Check if product is already in cart
                var existingCartItem = await _cartItemRepository.GetAll()
                    .FirstOrDefaultAsync(ci =>
                        ci.UserId == request.UserId &&
                        ci.ProductType == dto.ProductType &&
                        ci.ProductId == dto.ProductId &&
                        !ci.IsDeleted,
                        cancellationToken);

                CartItem cartItem;

                if (existingCartItem != null)
                {
                    // ✅ Update quantity using SaveInclude for partial update
                    var newQuantity = existingCartItem.Quantity + dto.Quantity;

                    if (newQuantity > availableStock)
                    {
                        return RequestResponse<AddedCartItemDto>.Fail(
                            $"Cannot add {dto.Quantity} more. Only {availableStock - existingCartItem.Quantity} items left.",
                            $"لا يمكنك إضافة {dto.Quantity}. الكمية المتاحة فقط هي {availableStock - existingCartItem.Quantity}"
                        );
                    }

                    // Update only specific properties using SaveInclude
                    existingCartItem.Quantity = newQuantity;
                    existingCartItem.UpdatedAt = DateTime.UtcNow;

                    // Use SaveInclude to update only Quantity and UpdatedAt properties
                    _cartItemRepository.SaveInclude(existingCartItem, "Quantity", "UpdatedAt");
                    cartItem = existingCartItem;
                }
                else
                {
                    // ✅ Add new item
                    if (dto.Quantity > availableStock)
                    {
                        return RequestResponse<AddedCartItemDto>.Fail(
                            $"Only {availableStock} in stock",
                            $"المتاح فقط {availableStock}"
                        );
                    }

                    cartItem = new CartItem
                    {
                        UserId = request.UserId,
                        ProductType = dto.ProductType,
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity,
                        AddedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _cartItemRepository.AddAsync(cartItem);
                }

                await _unitOfWork.SaveChangesAsync();

                // ✅ Cart Summary
                var summary = await GetCartSummary(request.UserId, cancellationToken);

                var response = new AddedCartItemDto
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    ProductType = cartItem.ProductType,
                    ProductName = productName,
                    Quantity = cartItem.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = unitPrice * cartItem.Quantity,
                    AddedAt = cartItem.AddedAt,
                    CartSummary = summary
                };

                return RequestResponse<AddedCartItemDto>.Success(
                    response,
                    existingCartItem != null ? "Cart item quantity updated successfully" : "Item added to cart successfully",
                    existingCartItem != null ? "تم تحديث كمية عنصر السلة بنجاح" : "تم إضافة العنصر إلى السلة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<AddedCartItemDto>.Fail(
                    "An error occurred while updating cart",
                    "حدث خطأ أثناء تحديث السلة"
                );
            }
        }

        private async Task<(RequestResponse<AddedCartItemDto> validationResult, string productName, decimal unitPrice, int availableStock)>
            ValidateProduct(ProductType productType, int productId, CancellationToken cancellationToken)
        {
            switch (productType)
            {
                case ProductType.LaptopVariant:
                    var laptopVariant = await _laptopVariantRepository.GetAll()
                        .Include(lv => lv.Laptop)
                            .ThenInclude(l => l.Brand)
                        .FirstOrDefaultAsync(lv => lv.Id == productId && lv.IsActive, cancellationToken);

                    if (laptopVariant == null)
                    {
                        return (RequestResponse<AddedCartItemDto>.Fail(
                            "Laptop variant not found or inactive",
                            "نوع اللابتوب غير موجود أو غير نشط"
                        ), "", 0, 0);
                    }

                    var productName = $"{laptopVariant.Laptop.Brand.Name} {laptopVariant.Laptop.ModelName} - {laptopVariant.RAM}GB RAM, {laptopVariant.StorageCapacityGB}GB {laptopVariant.StorageType}";
                    var availableStock = laptopVariant.StockQuantity - laptopVariant.ReservedQuantity;

                    return (RequestResponse<AddedCartItemDto>.Success(null!, "", ""), productName, laptopVariant.CurrentPrice, availableStock);

                default:
                    return (RequestResponse<AddedCartItemDto>.Fail(
                        "Unsupported product type",
                        "نوع المنتج غير مدعوم"
                    ), "", 0, 0);
            }
        }

        private async Task<CartSummaryDto> GetCartSummary(string userId, CancellationToken cancellationToken)
        {
            // Get cart items
            var cartItems = await _cartItemRepository.GetAll()
                .Where(ci => ci.UserId == userId && !ci.IsDeleted)
                .ToListAsync(cancellationToken);

            if (!cartItems.Any())
            {
                return new CartSummaryDto { TotalItems = 0, Total = 0 };
            }

            var totalItems = cartItems.Sum(ci => ci.Quantity);
            decimal total = 0;

            // Get all laptop variant IDs from cart items
            var laptopVariantIds = cartItems
                .Where(ci => ci.ProductType == ProductType.LaptopVariant)
                .Select(ci => ci.ProductId)
                .Distinct()
                .ToList();

            // Load all laptop variants in one query
            var laptopVariants = await _laptopVariantRepository.GetAll()
                .Where(lv => laptopVariantIds.Contains(lv.Id))
                .ToDictionaryAsync(lv => lv.Id, lv => lv.CurrentPrice, cancellationToken);

            foreach (var cartItem in cartItems)
            {
                decimal unitPrice = 0;

                switch (cartItem.ProductType)
                {
                    case ProductType.LaptopVariant:
                        if (laptopVariants.TryGetValue(cartItem.ProductId, out var price))
                        {
                            unitPrice = price;
                        }
                        break;

                    // Add pricing for other product types as needed
                    default:
                        continue;
                }

                total += unitPrice * cartItem.Quantity;
            }

            return new CartSummaryDto
            {
                TotalItems = totalItems,
                Total = total
            };
        }
    }
}