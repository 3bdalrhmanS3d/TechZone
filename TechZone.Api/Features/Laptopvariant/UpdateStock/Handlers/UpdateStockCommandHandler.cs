using MediatR;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.LaptopVariant.UpdateStock.Commands;
using TechZoneV1.Features.LaptopVariant.UpdateStock.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.UpdateStock.Handlers
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, RequestResponse<StockUpdateViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStockCommandHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<StockUpdateViewModel>> Handle(
            UpdateStockCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var updateDto = request.UpdateDto;

                // Get existing laptop variant
                var existingVariant = await _laptopVariantRepository.GetByIdAsync(request.Id);
                if (existingVariant == null)
                {
                    return RequestResponse<StockUpdateViewModel>.Fail(
                        "Laptop variant not found",
                        "نوع اللابتوب غير موجود"
                    );
                }

                // Store previous stock for response
                var previousStock = existingVariant.StockQuantity;
                var newStock = CalculateNewStock(previousStock, updateDto.Quantity, updateDto.Operation);

                // Validate stock doesn't go negative
                if (newStock < 0)
                {
                    return RequestResponse<StockUpdateViewModel>.Fail(
                        "Stock cannot be negative",
                        "لا يمكن أن يكون المخزون سالباً"
                    );
                }

                // Create a temporary entity with only the properties we want to update
                var updatedVariant = new TechZone.Domain.Entities.LaptopVariant
                {
                    Id = existingVariant.Id,
                    StockQuantity = newStock,
                    UpdatedAt = DateTime.UtcNow
                    // Note: We don't set other properties, so they won't be updated
                };

                // Use SaveInclude to update only specified properties
                var propertiesToUpdate = new[]
                {
                    nameof(TechZone.Domain.Entities.LaptopVariant.StockQuantity),
                    nameof(TechZone.Domain.Entities.LaptopVariant.UpdatedAt)
                };

                _laptopVariantRepository.SaveInclude(updatedVariant, propertiesToUpdate);
                await _unitOfWork.SaveChangesAsync();

                // Refresh the entity to get the updated values
                var refreshedVariant = await _laptopVariantRepository.GetByIdAsync(request.Id);

                // Calculate available quantity and stock status
                var availableQuantity = refreshedVariant.StockQuantity - refreshedVariant.ReservedQuantity;
                var stockStatus = GetStockStatus(refreshedVariant);

                // Map to ViewModel
                var viewModel = new StockUpdateViewModel
                {
                    Id = refreshedVariant.Id,
                    PreviousStock = previousStock,
                    NewStock = newStock,
                    ReservedQuantity = refreshedVariant.ReservedQuantity,
                    AvailableQuantity = availableQuantity,
                    StockStatus = stockStatus,
                    UpdatedAt = refreshedVariant.UpdatedAt ?? DateTime.UtcNow
                };

                return RequestResponse<StockUpdateViewModel>.Success(
                    viewModel,
                    "Stock updated successfully",
                    "تم تحديث المخزون بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<StockUpdateViewModel>.Fail(
                    "An error occurred while updating stock",
                    "حدث خطأ أثناء تحديث المخزون"
                );
            }
        }

        private static int CalculateNewStock(int currentStock, int quantity, string operation)
        {
            return operation.ToLower() switch
            {
                "add" => currentStock + quantity,
                "subtract" => currentStock - quantity,
                "set" => quantity,
                _ => currentStock
            };
        }

        private static string GetStockStatus(TechZone.Domain.Entities.LaptopVariant variant)
        {
            var availableQuantity = variant.StockQuantity - variant.ReservedQuantity;

            if (availableQuantity <= 0) return "OutOfStock";
            if (availableQuantity <= variant.ReorderLevel) return "LowStock";
            return "InStock";
        }
    }
}