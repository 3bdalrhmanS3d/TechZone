using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.Commands;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.Dtos;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.UpdateVariant.Handlers
{
    public class UpdateLaptopVariantCommandHandler : IRequestHandler<UpdateLaptopVariantCommand, RequestResponse<UpdateLaptopVariantViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<PriceHistory> _priceHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLaptopVariantCommandHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<PriceHistory> priceHistoryRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _priceHistoryRepository = priceHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<UpdateLaptopVariantViewModel>> Handle(
            UpdateLaptopVariantCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var updateDto = request.UpdateDto;

                // Get existing laptop variant
                var existingVariant = await _laptopVariantRepository.GetByIdAsync(request.Id);
                if (existingVariant == null)
                {
                    return RequestResponse<UpdateLaptopVariantViewModel>.Fail(
                        "Laptop variant not found",
                        "نوع اللابتوب غير موجود"
                    );
                }

                // Check if at least one property is being updated
                if (!IsAnyPropertyUpdated(updateDto))
                {
                    return RequestResponse<UpdateLaptopVariantViewModel>.Fail(
                        "At least one property must be provided for update",
                        "يجب تقديم خاصية واحدة على الأقل للتحديث"
                    );
                }

                // Check if SKU already exists (only if SKU is being updated)
                if (!string.IsNullOrEmpty(updateDto.Sku) && updateDto.Sku != existingVariant.SKU)
                {
                    var skuExists = await _laptopVariantRepository.GetAll()
                        .AnyAsync(lv => lv.SKU == updateDto.Sku && lv.Id != request.Id, cancellationToken);

                    if (skuExists)
                    {
                        return RequestResponse<UpdateLaptopVariantViewModel>.Fail(
                            "SKU already exists",
                            "رقم SKU موجود مسبقاً"
                        );
                    }
                }

                // Store old price for comparison (only if price is being updated)
                var oldPrice = existingVariant.CurrentPrice;
                var priceChanged = updateDto.CurrentPrice.HasValue &&
                                  updateDto.CurrentPrice.Value != oldPrice;

                // Prepare list of properties to update
                var propertiesToUpdate = new List<string>();

                // Update only provided properties
                if (!string.IsNullOrEmpty(updateDto.Sku))
                {
                    existingVariant.SKU = updateDto.Sku;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.SKU));
                }

                if (updateDto.Ram.HasValue)
                {
                    existingVariant.RAM = updateDto.Ram.Value;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.RAM));
                }

                if (updateDto.Storage.HasValue)
                {
                    existingVariant.StorageCapacityGB = updateDto.Storage.Value;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.StorageCapacityGB));
                }

                if (!string.IsNullOrEmpty(updateDto.StorageType))
                {
                    existingVariant.StorageType = updateDto.StorageType;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.StorageType));
                }

                if (updateDto.CurrentPrice.HasValue)
                {
                    existingVariant.CurrentPrice = updateDto.CurrentPrice.Value;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.CurrentPrice));
                }

                if (updateDto.StockQuantity.HasValue)
                {
                    existingVariant.StockQuantity = updateDto.StockQuantity.Value;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.StockQuantity));
                }

                if (updateDto.ReorderLevel.HasValue)
                {
                    existingVariant.ReorderLevel = updateDto.ReorderLevel.Value;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.ReorderLevel));
                }

                if (updateDto.IsActive.HasValue)
                {
                    existingVariant.IsActive = updateDto.IsActive.Value;
                    propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.IsActive));
                }

                // Always update UpdatedAt
                existingVariant.UpdatedAt = DateTime.UtcNow;
                propertiesToUpdate.Add(nameof(TechZone.Domain.Entities.LaptopVariant.UpdatedAt));

                // Use SaveInclude to update only specified properties
                _laptopVariantRepository.SaveInclude(existingVariant, propertiesToUpdate.ToArray());

                // Create price history record if price changed
                if (priceChanged && updateDto.CurrentPrice.HasValue)
                {
                    var priceHistory = new PriceHistory
                    {
                        ProductType = ProductType.LaptopVariant,
                        ProductId = existingVariant.Id,
                        OldPrice = oldPrice,
                        NewPrice = updateDto.CurrentPrice.Value,
                        ChangeReason = "Price update",
                        EffectiveFrom = DateTime.UtcNow,
                        ChangedByUserId = "system" // You might want to get this from the current user
                    };

                    await _priceHistoryRepository.AddAsync(priceHistory);
                }

                await _unitOfWork.SaveChangesAsync();

                // Map to ViewModel
                var viewModel = new UpdateLaptopVariantViewModel
                {
                    Id = existingVariant.Id,
                    Sku = existingVariant.SKU,
                    LaptopId = existingVariant.LaptopId,
                    Ram = existingVariant.RAM,
                    Storage = existingVariant.StorageCapacityGB,
                    StorageType = existingVariant.StorageType,
                    CurrentPrice = existingVariant.CurrentPrice,
                    OldPrice = oldPrice,
                    StockQuantity = existingVariant.StockQuantity,
                    ReservedQuantity = existingVariant.ReservedQuantity,
                    AvailableQuantity = existingVariant.StockQuantity - existingVariant.ReservedQuantity,
                    ReorderLevel = existingVariant.ReorderLevel,
                    IsActive = existingVariant.IsActive,
                    CreatedAt = existingVariant.CreatedAt,
                    UpdatedAt = existingVariant.UpdatedAt ?? DateTime.UtcNow
                };

                return RequestResponse<UpdateLaptopVariantViewModel>.Success(
                    viewModel,
                    "Laptop variant updated successfully",
                    "تم تحديث نوع اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<UpdateLaptopVariantViewModel>.Fail(
                    "An error occurred while updating laptop variant",
                    "حدث خطأ أثناء تحديث نوع اللابتوب"
                );
            }
        }

        private static bool IsAnyPropertyUpdated(UpdateLaptopVariantDto updateDto)
        {
            return !string.IsNullOrEmpty(updateDto.Sku) ||
                   updateDto.Ram.HasValue ||
                   updateDto.Storage.HasValue ||
                   !string.IsNullOrEmpty(updateDto.StorageType) ||
                   updateDto.CurrentPrice.HasValue ||
                   updateDto.StockQuantity.HasValue ||
                   updateDto.ReorderLevel.HasValue ||
                   updateDto.IsActive.HasValue;
        }
    }
}