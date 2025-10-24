using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Commands;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Dtos;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Handlers
{
    public class BulkUpdateStockCommandHandler : IRequestHandler<BulkUpdateStockCommand, RequestResponse<BulkUpdateStockViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BulkUpdateStockCommandHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<BulkUpdateStockViewModel>> Handle(
            BulkUpdateStockCommand request,
            CancellationToken cancellationToken)
        {
            var results = new List<StockUpdateResultViewModel>();
            var successCount = 0;
            var failedCount = 0;

            try
            {
                var bulkUpdateDto = request.BulkUpdateDto;

                // Get all variant IDs from the request to check existence
                var variantIds = bulkUpdateDto.Updates.Select(u => u.VariantId).ToList();

                // Check which variants exist and get their current stock
                var existingVariants = await _laptopVariantRepository.GetAll()
                    .Where(lv => variantIds.Contains(lv.Id))
                    .Select(lv => new { lv.Id, lv.StockQuantity })
                    .ToDictionaryAsync(lv => lv.Id, lv => lv.StockQuantity, cancellationToken);

                // Process each update
                foreach (var update in bulkUpdateDto.Updates)
                {
                    var result = await ProcessSingleUpdate(update, existingVariants, cancellationToken);
                    results.Add(result);

                    if (result.Success)
                        successCount++;
                    else
                        failedCount++;
                }

                var viewModel = new BulkUpdateStockViewModel
                {
                    SuccessCount = successCount,
                    FailedCount = failedCount,
                    Results = results
                };

                return RequestResponse<BulkUpdateStockViewModel>.Success(
                    viewModel,
                    "Bulk stock update completed successfully",
                    "تم تحديث المخزون بالجملة بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<BulkUpdateStockViewModel>.Fail(
                    "An error occurred while performing bulk stock update",
                    "حدث خطأ أثناء تحديث المخزون بالجملة"
                );
            }
        }

        private async Task<StockUpdateResultViewModel> ProcessSingleUpdate(
            StockUpdateItemDto update,
            Dictionary<int, int> existingVariants,
            CancellationToken cancellationToken)
        {
            try
            {
                // Check if variant exists
                if (!existingVariants.ContainsKey(update.VariantId))
                {
                    return new StockUpdateResultViewModel
                    {
                        VariantId = update.VariantId,
                        Success = false,
                        PreviousStock = 0,
                        NewStock = 0,
                        Message = "Variant not found"
                    };
                }

                var previousStock = existingVariants[update.VariantId];
                var newStock = CalculateNewStock(previousStock, update.Quantity, update.Operation);

                // Validate stock doesn't go negative
                if (newStock < 0)
                {
                    return new StockUpdateResultViewModel
                    {
                        VariantId = update.VariantId,
                        Success = false,
                        PreviousStock = previousStock,
                        NewStock = newStock,
                        Message = "Stock cannot be negative"
                    };
                }

                // Use ExecuteUpdate for type-safe database updates
                var rowsAffected = update.Operation.ToLower() switch
                {
                    "add" => await _laptopVariantRepository.GetAll()
                        .Where(lv => lv.Id == update.VariantId && lv.IsActive)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(x => x.StockQuantity, x => x.StockQuantity + update.Quantity)
                            .SetProperty(x => x.UpdatedAt, DateTime.UtcNow),
                        cancellationToken),

                    "subtract" => await _laptopVariantRepository.GetAll()
                        .Where(lv => lv.Id == update.VariantId && lv.IsActive)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(x => x.StockQuantity, x => x.StockQuantity - update.Quantity)
                            .SetProperty(x => x.UpdatedAt, DateTime.UtcNow),
                        cancellationToken),

                    "set" => await _laptopVariantRepository.GetAll()
                        .Where(lv => lv.Id == update.VariantId && lv.IsActive)
                        .ExecuteUpdateAsync(p => p
                            .SetProperty(x => x.StockQuantity, newStock)
                            .SetProperty(x => x.UpdatedAt, DateTime.UtcNow),
                        cancellationToken),

                    _ => throw new ArgumentException($"Invalid operation: {update.Operation}")
                };

                if (rowsAffected == 0)
                {
                    return new StockUpdateResultViewModel
                    {
                        VariantId = update.VariantId,
                        Success = false,
                        PreviousStock = previousStock,
                        NewStock = newStock,
                        Message = "Variant is not active or does not exist"
                    };
                }

                return new StockUpdateResultViewModel
                {
                    VariantId = update.VariantId,
                    Success = true,
                    PreviousStock = previousStock,
                    NewStock = newStock,
                    Message = "Stock updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new StockUpdateResultViewModel
                {
                    VariantId = update.VariantId,
                    Success = false,
                    PreviousStock = 0,
                    NewStock = 0,
                    Message = $"Error: {ex.Message}"
                };
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
    }
}