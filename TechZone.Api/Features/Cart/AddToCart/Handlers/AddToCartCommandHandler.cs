using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.AddToCart.Commands;
using TechZoneV1.Features.Cart.AddToCart.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.AddToCart.Handlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, RequestResponse<AddedCartItemDto>>
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

        public async Task<RequestResponse<AddedCartItemDto>> Handle(
            AddToCartCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var requestDto = request.RequestDto;

                // Validate product exists and is available
                var (validationResult, productName, unitPrice, availableStock) = await ValidateProduct(
                    requestDto.ProductType,
                    requestDto.ProductId,
                    cancellationToken);

                if (!validationResult.IsSuccess)
                {
                    return validationResult;
                }

                // Check stock availability
                if (requestDto.Quantity > availableStock)
                {
                    return RequestResponse<AddedCartItemDto>.Fail(
                        $"Not enough stock available. Only {availableStock} items in stock.",
                        $"لا يوجد مخزون كافي. فقط {availableStock} عنصر متاح."
                    );
                }

                // Check if item already exists in cart
                var existingCartItem = await _cartItemRepository.GetAll()
                    .Where(ci => ci.UserId == request.UserId &&
                                 ci.ProductType == requestDto.ProductType &&
                                 ci.ProductId == requestDto.ProductId &&
                                 !ci.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                CartItem cartItem;

                if (existingCartItem != null)
                {
                    // Update quantity if item already exists
                    existingCartItem.Quantity += requestDto.Quantity;
                    existingCartItem.UpdatedAt = DateTime.UtcNow;
                    cartItem = existingCartItem;
                }
                else
                {
                    // Create new cart item
                    cartItem = new CartItem
                    {
                        UserId = request.UserId,
                        ProductType = requestDto.ProductType,
                        ProductId = requestDto.ProductId,
                        Quantity = requestDto.Quantity,
                        AddedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _cartItemRepository.AddAsync(cartItem);
                }

                await _unitOfWork.SaveChangesAsync();

                // Get updated cart summary
                var cartSummary = await GetCartSummary(request.UserId, cancellationToken);

                // Return DTO
                var addedCartItemDto = new AddedCartItemDto
                {
                    Id = cartItem.Id,
                    ProductType = cartItem.ProductType,
                    ProductId = cartItem.ProductId,
                    ProductName = productName,
                    Quantity = cartItem.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = unitPrice * cartItem.Quantity,
                    AddedAt = cartItem.AddedAt,
                    CartSummary = cartSummary
                };

                return RequestResponse<AddedCartItemDto>.Success(
                    addedCartItemDto,
                    "Item added to cart successfully",
                    "تم إضافة العنصر إلى السلة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<AddedCartItemDto>.Fail(
                    "An error occurred while adding item to cart",
                    "حدث خطأ أثناء إضافة العنصر إلى السلة"
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
            // Get cart items without the problematic Include
            var cartItems = await _cartItemRepository.GetAll()
                .Where(ci => ci.UserId == userId && !ci.IsDeleted)
                .ToListAsync(cancellationToken);

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
                        // Unknown product type - skip or handle accordingly
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