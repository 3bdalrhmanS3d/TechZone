using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Cart.UpdateCartItem.Commands;
using TechZoneV1.Features.Cart.UpdateCartItem.Dtos;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Cart.UpdateCartItem.Handlers
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, RequestResponse<UpdatedCartItemDto>>
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartItemCommandHandler(
            IBaseRepository<CartItem> cartItemRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _cartItemRepository = cartItemRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<UpdatedCartItemDto>> Handle(
            UpdateCartItemCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var requestDto = request.RequestDto;

                // Get the cart item with user validation
                var cartItem = await _cartItemRepository.GetAll()
                    .Where(ci => ci.Id == request.ItemId &&
                                 ci.UserId == request.UserId &&
                                 !ci.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (cartItem == null)
                {
                    return RequestResponse<UpdatedCartItemDto>.Fail(
                        "Cart item not found",
                        "عنصر السلة غير موجود"
                    );
                }

                // Validate product exists and check stock availability
                var (validationResult, unitPrice, availableStock) = await ValidateProductAndStock(
                    cartItem.ProductType,
                    cartItem.ProductId,
                    requestDto.Quantity,
                    cancellationToken);

                if (!validationResult.IsSuccess)
                {
                    return validationResult;
                }

                // Update only specific properties using SaveInclude
                cartItem.Quantity = requestDto.Quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;

                // Use SaveInclude to update only Quantity and UpdatedAt
                _cartItemRepository.SaveInclude(cartItem, "Quantity", "UpdatedAt");
                await _unitOfWork.SaveChangesAsync();

                // Return DTO
                var updatedCartItemDto = new UpdatedCartItemDto
                {
                    Id = cartItem.Id,
                    ProductType = cartItem.ProductType,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = unitPrice * cartItem.Quantity,
                    UpdatedAt = cartItem.UpdatedAt ?? DateTime.UtcNow
                };

                return RequestResponse<UpdatedCartItemDto>.Success(
                    updatedCartItemDto,
                    "Cart item updated successfully",
                    "تم تحديث عنصر السلة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<UpdatedCartItemDto>.Fail(
                    "An error occurred while updating cart item",
                    "حدث خطأ أثناء تحديث عنصر السلة"
                );
            }
        }

        private async Task<(RequestResponse<UpdatedCartItemDto> validationResult, decimal unitPrice, int availableStock)>
            ValidateProductAndStock(ProductType productType, int productId, int requestedQuantity, CancellationToken cancellationToken)
        {
            switch (productType)
            {
                case ProductType.LaptopVariant:
                    var laptopVariant = await _laptopVariantRepository.GetAll()
                        .FirstOrDefaultAsync(lv => lv.Id == productId && lv.IsActive, cancellationToken);

                    if (laptopVariant == null)
                    {
                        return (RequestResponse<UpdatedCartItemDto>.Fail(
                            "Product not found or inactive",
                            "المنتج غير موجود أو غير نشط"
                        ), 0, 0);
                    }

                    var availableStock = laptopVariant.StockQuantity - laptopVariant.ReservedQuantity;

                    if (requestedQuantity > availableStock)
                    {
                        return (RequestResponse<UpdatedCartItemDto>.Fail(
                            $"Not enough stock available. Only {availableStock} items in stock.",
                            $"لا يوجد مخزون كافي. فقط {availableStock} عنصر متاح."
                        ), laptopVariant.CurrentPrice, availableStock);
                    }

                    return (RequestResponse<UpdatedCartItemDto>.Success(null!, "", ""), laptopVariant.CurrentPrice, availableStock);

                // Add other product types as needed
                default:
                    return (RequestResponse<UpdatedCartItemDto>.Fail(
                        "Unsupported product type",
                        "نوع المنتج غير مدعوم"
                    ), 0, 0);
            }
        }
    }
}