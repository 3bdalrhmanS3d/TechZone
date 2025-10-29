using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.GetCart.Dtos;
using TechZoneV1.Features.Cart.GetCart.Queries;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.GetCart.Handlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, RequestResponse<CartDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IBaseRepository<ProductDiscount> _productDiscountRepository;
        private readonly IBaseRepository<Discount> _discountRepository;

        public GetCartQueryHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<Laptop> laptopRepository,
            IBaseRepository<ProductDiscount> productDiscountRepository,
            IBaseRepository<Discount> discountRepository)
        {
            _cartItemRepository = cartItemRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _laptopRepository = laptopRepository;
            _productDiscountRepository = productDiscountRepository;
            _discountRepository = discountRepository;
        }

        public async Task<RequestResponse<CartDto>> Handle(
            GetCartQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get cart items for user
                var cartItems = await _cartItemRepository.GetAll()
                    .Where(ci => ci.UserId == request.UserId && !ci.IsDeleted)
                    .OrderByDescending(ci => ci.AddedAt)
                    .ToListAsync(cancellationToken);

                if (!cartItems.Any())
                {
                    return RequestResponse<CartDto>.Success(
                        new CartDto(),
                        "Cart is empty",
                        "السلة فارغة"
                    );
                }

                // Get active discounts
                var currentDate = DateTime.UtcNow;
                var activeDiscounts = await _discountRepository.GetAll()
                    .Where(d => d.IsActive && d.StartDate <= currentDate && d.EndDate >= currentDate)
                    .ToListAsync(cancellationToken);

                var productDiscounts = await _productDiscountRepository.GetAll()
                    .Where(pd => activeDiscounts.Select(ad => ad.Id).Contains(pd.DiscountId))
                    .ToListAsync(cancellationToken);

                // Process cart items
                var cartItemDtos = new List<CartItemDto>();
                decimal subtotal = 0;
                decimal totalDiscount = 0;

                foreach (var cartItem in cartItems)
                {
                    var itemDto = await MapCartItemToDto(
                        cartItem,
                        productDiscounts,
                        activeDiscounts,
                        cancellationToken);

                    if (itemDto != null)
                    {
                        cartItemDtos.Add(itemDto);
                        subtotal += itemDto.TotalPrice;
                        totalDiscount += itemDto.DiscountAmount * itemDto.Quantity;
                    }
                }

                // Calculate totals and return DTO
                var cartDto = new CartDto
                {
                    Items = cartItemDtos,
                    TotalItems = cartItemDtos.Sum(item => item.Quantity),
                    Subtotal = subtotal,
                    Discount = totalDiscount,
                    Tax = 0, // Add tax calculation logic if needed
                    Shipping = 0, // Add shipping calculation logic if needed
                    Total = subtotal - totalDiscount,
                    AppliedDiscountCode = null // Implement discount code logic if needed
                };

                return RequestResponse<CartDto>.Success(
                    cartDto,
                    "Cart fetched successfully",
                    "تم جلب السلة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<CartDto>.Fail(
                    "An error occurred while fetching cart",
                    "حدث خطأ أثناء جلب السلة"
                );
            }
        }

        private async Task<CartItemDto?> MapCartItemToDto(
            CartItem cartItem,
            List<ProductDiscount> productDiscounts,
            List<Discount> activeDiscounts,
            CancellationToken cancellationToken)
        {
            string productName = string.Empty;
            string sku = string.Empty;
            decimal unitPrice = 0;
            decimal discountAmount = 0;
            int stockAvailable = 0;
            string imageUrl = string.Empty;

            switch (cartItem.ProductType)
            {
                case ProductType.LaptopVariant:
                    var laptopVariant = await _laptopVariantRepository.GetAll()
                        .Include(lv => lv.Laptop)
                            .ThenInclude(l => l.Images)
                        .Include(lv => lv.Laptop)
                            .ThenInclude(l => l.Brand)
                        .FirstOrDefaultAsync(lv => lv.Id == cartItem.ProductId && lv.IsActive, cancellationToken);

                    if (laptopVariant == null)
                        return null;

                    productName = $"{laptopVariant.Laptop.Brand.Name} {laptopVariant.Laptop.ModelName} - {laptopVariant.RAM}GB RAM, {laptopVariant.StorageCapacityGB}GB {laptopVariant.StorageType}";
                    sku = laptopVariant.SKU;
                    unitPrice = laptopVariant.CurrentPrice;
                    stockAvailable = laptopVariant.StockQuantity - laptopVariant.ReservedQuantity;
                    imageUrl = laptopVariant.Laptop.Images
                        .Where(img => img.IsMain)
                        .OrderBy(img => img.DisplayOrder)
                        .Select(img => img.ImageUrl)
                        .FirstOrDefault() ?? string.Empty;

                    // Calculate discount for laptop variant
                    var laptopDiscount = productDiscounts
                        .FirstOrDefault(pd => pd.ProductType == ProductType.LaptopVariant && pd.ProductId == cartItem.ProductId);

                    if (laptopDiscount != null)
                    {
                        var discount = activeDiscounts.FirstOrDefault(d => d.Id == laptopDiscount.DiscountId);
                        if (discount != null)
                        {
                            discountAmount = CalculateDiscountAmount(unitPrice, discount);
                        }
                    }
                    break;

                // Add other product types (Accessory, etc.) as needed
                default:
                    return null;
            }

            var totalPrice = (unitPrice - discountAmount) * cartItem.Quantity;

            return new CartItemDto
            {
                Id = cartItem.Id,
                ProductType = cartItem.ProductType,
                ProductId = cartItem.ProductId,
                ProductName = productName,
                SKU = sku,
                Quantity = cartItem.Quantity,
                UnitPrice = unitPrice,
                DiscountAmount = discountAmount,
                TotalPrice = totalPrice,
                StockAvailable = stockAvailable,
                Image = imageUrl,
                AddedAt = cartItem.AddedAt
            };
        }

        private decimal CalculateDiscountAmount(decimal unitPrice, Discount discount)
        {
            if (discount.DiscountType == DiscountType.Percentage)
            {
                var discountValue = discount.Value / 100m;
                var calculatedDiscount = unitPrice * discountValue;

                if (discount.MaxDiscountAmount > 0 && calculatedDiscount > discount.MaxDiscountAmount)
                {
                    return discount.MaxDiscountAmount;
                }
                return calculatedDiscount;
            }
            else // FixedAmount
            {
                return discount.Value;
            }
        }
    }
}