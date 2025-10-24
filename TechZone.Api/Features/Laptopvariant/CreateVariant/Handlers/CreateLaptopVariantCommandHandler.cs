using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.LaptopVariant.CreateVariant.Commands;
using TechZoneV1.Features.LaptopVariant.CreateVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.CreateVariant.Handlers
{
    public class CreateLaptopVariantCommandHandler : IRequestHandler<CreateLaptopVariantCommand, RequestResponse<LaptopVariantViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLaptopVariantCommandHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IBaseRepository<Laptop> laptopRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _laptopRepository = laptopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<LaptopVariantViewModel>> Handle(
            CreateLaptopVariantCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var createDto = request.CreateDto;

                // Validate that the laptop exists
                var laptopExists = await _laptopRepository.GetAll()
                    .AnyAsync(l => l.Id == createDto.LaptopId && l.IsActive, cancellationToken);

                if (!laptopExists)
                {
                    return RequestResponse<LaptopVariantViewModel>.Fail(
                        "Laptop not found or is not active",
                        "اللابتوب غير موجود أو غير نشط"
                    );
                }

                // Check if SKU already exists
                var skuExists = await _laptopVariantRepository.GetAll()
                    .AnyAsync(lv => lv.SKU == createDto.Sku, cancellationToken);

                if (skuExists)
                {
                    return RequestResponse<LaptopVariantViewModel>.Fail(
                        "SKU already exists",
                        "رقم SKU موجود مسبقاً"
                    );
                }

                // Create new laptop variant
                var laptopVariant = new TechZone.Domain.Entities.LaptopVariant
                {
                    LaptopId = createDto.LaptopId,
                    SKU = createDto.Sku,
                    RAM = createDto.Ram,
                    StorageCapacityGB = createDto.Storage,
                    StorageType = createDto.StorageType,
                    CurrentPrice = createDto.CurrentPrice,
                    StockQuantity = createDto.StockQuantity,
                    ReservedQuantity = 0, // Default to 0 for new variants
                    ReorderLevel = createDto.ReorderLevel,
                    IsActive = createDto.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Add to repository
                await _laptopVariantRepository.AddAsync(laptopVariant);
                await _unitOfWork.SaveChangesAsync();

                // Map to ViewModel
                var viewModel = new LaptopVariantViewModel
                {
                    Id = laptopVariant.Id,
                    Sku = laptopVariant.SKU,
                    LaptopId = laptopVariant.LaptopId,
                    Ram = laptopVariant.RAM,
                    Storage = laptopVariant.StorageCapacityGB,
                    StorageType = laptopVariant.StorageType,
                    CurrentPrice = laptopVariant.CurrentPrice,
                    StockQuantity = laptopVariant.StockQuantity,
                    ReservedQuantity = laptopVariant.ReservedQuantity,
                    AvailableQuantity = laptopVariant.StockQuantity - laptopVariant.ReservedQuantity,
                    ReorderLevel = laptopVariant.ReorderLevel,
                    IsActive = laptopVariant.IsActive,
                    CreatedAt = laptopVariant.CreatedAt,
                    UpdatedAt = laptopVariant.UpdatedAt ?? DateTime.UtcNow
                };

                return RequestResponse<LaptopVariantViewModel>.Success(
                    viewModel,
                    "Laptop variant created successfully",
                    "تم إنشاء نوع اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<LaptopVariantViewModel>.Fail(
                    "An error occurred while creating laptop variant",
                    "حدث خطأ أثناء إنشاء نوع اللابتوب"
                );
            }
        }
    }
}