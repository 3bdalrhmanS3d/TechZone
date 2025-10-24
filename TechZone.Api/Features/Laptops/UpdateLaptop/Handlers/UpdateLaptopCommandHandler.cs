using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Laptops.UpdateLaptop.Commands;
using TechZoneV1.Features.Laptops.UpdateLaptop.Dtos;
using TechZoneV1.Features.Laptops.UpdateLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.UpdateLaptop.Handlers
{
    public class UpdateLaptopCommandHandler : IRequestHandler<UpdateLaptopCommand, RequestResponse<MainLaptopUpdatedViewModel>>
    {
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IBaseRepository<Brand> _brandRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLaptopCommandHandler(
            IBaseRepository<Laptop> laptopRepository,
            IBaseRepository<Brand> brandRepository,
            IBaseRepository<TechZone.Domain.Entities.Category> categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopRepository = laptopRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<MainLaptopUpdatedViewModel>> Handle(
            UpdateLaptopCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var updateLaptopDto = request.UpdateLaptopDto;

                // Check if at least one field is provided for update
                if (!IsAnyFieldProvided(updateLaptopDto))
                {
                    return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                        "At least one field must be provided for update",
                        "يجب تقديم حقل واحد على الأقل للتحديث"
                    );
                }

                // Get existing laptop
                var laptop = await _laptopRepository.GetAll()
                    .Include(l => l.Brand)
                    .Include(l => l.Category)
                    .Where(l => l.Id == request.Id && !l.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (laptop == null)
                {
                    return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                        "Laptop not found",
                        "اللابتوب غير موجود"
                    );
                }

                // Validate brand if provided
                if (updateLaptopDto.BrandId.HasValue)
                {
                    var brand = await _brandRepository.GetAll()
                        .Where(b => b.Id == updateLaptopDto.BrandId.Value && b.IsActive)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (brand == null)
                    {
                        return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                            "Brand not found or inactive",
                            "العلامة التجارية غير موجودة أو غير مفعلة"
                        );
                    }
                }

                // Validate category if provided
                if (updateLaptopDto.CategoryId.HasValue)
                {
                    var category = await _categoryRepository.GetAll()
                        .Where(c => c.Id == updateLaptopDto.CategoryId.Value && c.IsActive)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (category == null)
                    {
                        return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                            "Category not found or inactive",
                            "الفئة غير موجودة أو غير مفعلة"
                        );
                    }
                }

                // Check for duplicate model name if provided
                if (!string.IsNullOrEmpty(updateLaptopDto.ModelName) &&
                    updateLaptopDto.ModelName != laptop.ModelName)
                {
                    var existingLaptop = await _laptopRepository.GetAll()
                        .Where(l => l.ModelName == updateLaptopDto.ModelName &&
                                   l.Id != request.Id &&
                                   !l.IsDeleted)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (existingLaptop != null)
                    {
                        return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                            "A laptop with this model name already exists",
                            "يوجد لابتوب بنفس اسم الموديل مسبقاً"
                        );
                    }
                }

                // Get the list of properties to update
                var propertiesToUpdate = GetPropertiesToUpdate(updateLaptopDto);

                // Update fields only if they are provided (not null)
                UpdateLaptopFields(laptop, updateLaptopDto);

                // Update timestamp (always update UpdatedAt when any field changes)
                laptop.UpdatedAt = DateTime.UtcNow;
                propertiesToUpdate.Add(nameof(Laptop.UpdatedAt));

                // Use SaveInclude to update only the specified properties
                _laptopRepository.SaveInclude(laptop, propertiesToUpdate.ToArray());
                await _unitOfWork.SaveChangesAsync();

                // Reload the laptop with brand and category to get updated relations
                var updatedLaptop = await _laptopRepository.GetAll()
                    .Include(l => l.Brand)
                    .Include(l => l.Category)
                    .Where(l => l.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                // Map to ViewModel
                var response = MapToViewModel(updatedLaptop!);

                return RequestResponse<MainLaptopUpdatedViewModel>.Success(
                    response,
                    "Laptop updated successfully",
                    "تم تحديث اللابتوب بنجاح"
                );
            }
            catch (DbUpdateException ex)
            {
                // Log database exception
                return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                    "An error occurred while updating laptop data",
                    "حدث خطأ أثناء تحديث بيانات اللابتوب"
                );
            }
            catch (Exception ex)
            {
                // Log general exception
                return RequestResponse<MainLaptopUpdatedViewModel>.Fail(
                    "An error occurred while updating laptop",
                    "حدث خطأ أثناء تحديث اللابتوب"
                );
            }
        }

        private static bool IsAnyFieldProvided(UpdateMainLaptopDto dto)
        {
            return !string.IsNullOrEmpty(dto.ModelName) ||
                   dto.BrandId.HasValue ||
                   dto.CategoryId.HasValue ||
                   !string.IsNullOrEmpty(dto.Processor) ||
                   !string.IsNullOrEmpty(dto.GPU) ||
                   !string.IsNullOrEmpty(dto.ScreenSize) ||
                   dto.HasCamera.HasValue ||
                   dto.HasKeyboard.HasValue ||
                   dto.HasTouchScreen.HasValue ||
                   !string.IsNullOrEmpty(dto.Description) ||
                   !string.IsNullOrEmpty(dto.StoreLocation) ||
                   !string.IsNullOrEmpty(dto.StoreContact) ||
                   dto.ReleaseYear.HasValue ||
                   dto.IsActive.HasValue;
        }

        private static List<string> GetPropertiesToUpdate(UpdateMainLaptopDto dto)
        {
            var properties = new List<string>();

            if (!string.IsNullOrEmpty(dto.ModelName))
                properties.Add(nameof(Laptop.ModelName));

            if (dto.BrandId.HasValue)
                properties.Add(nameof(Laptop.BrandId));

            if (dto.CategoryId.HasValue)
                properties.Add(nameof(Laptop.CategoryId));

            if (!string.IsNullOrEmpty(dto.Processor))
                properties.Add(nameof(Laptop.Processor));

            if (dto.GPU != null)
                properties.Add(nameof(Laptop.GPU));

            if (dto.ScreenSize != null)
                properties.Add(nameof(Laptop.ScreenSize));

            if (dto.HasCamera.HasValue)
                properties.Add(nameof(Laptop.HasCamera));

            if (dto.HasKeyboard.HasValue)
                properties.Add(nameof(Laptop.HasKeyboard));

            if (dto.HasTouchScreen.HasValue)
                properties.Add(nameof(Laptop.HasTouchScreen));

            if (dto.Description != null)
                properties.Add(nameof(Laptop.Description));

            if (dto.StoreLocation != null)
                properties.Add(nameof(Laptop.StoreLocation));

            if (dto.StoreContact != null)
                properties.Add(nameof(Laptop.StoreContact));

            if (dto.ReleaseYear.HasValue)
                properties.Add(nameof(Laptop.ReleaseYear));

            if (dto.IsActive.HasValue)
                properties.Add(nameof(Laptop.IsActive));

            return properties;
        }

        private static void UpdateLaptopFields(Laptop laptop, UpdateMainLaptopDto dto)
        {
            // Only update fields that are provided (not null)
            if (!string.IsNullOrEmpty(dto.ModelName))
                laptop.ModelName = dto.ModelName;

            if (dto.BrandId.HasValue)
                laptop.BrandId = dto.BrandId.Value;

            if (dto.CategoryId.HasValue)
                laptop.CategoryId = dto.CategoryId.Value;

            if (!string.IsNullOrEmpty(dto.Processor))
                laptop.Processor = dto.Processor;

            if (dto.GPU != null) // Allow empty string
                laptop.GPU = dto.GPU;

            if (dto.ScreenSize != null) // Allow empty string
                laptop.ScreenSize = dto.ScreenSize;

            if (dto.HasCamera.HasValue)
                laptop.HasCamera = dto.HasCamera.Value;

            if (dto.HasKeyboard.HasValue)
                laptop.HasKeyboard = dto.HasKeyboard.Value;

            if (dto.HasTouchScreen.HasValue)
                laptop.HasTouchScreen = dto.HasTouchScreen.Value;

            if (dto.Description != null) // Allow empty string
                laptop.Description = dto.Description;

            if (dto.StoreLocation != null) // Allow empty string
                laptop.StoreLocation = dto.StoreLocation;

            if (dto.StoreContact != null) // Allow empty string
                laptop.StoreContact = dto.StoreContact;

            if (dto.ReleaseYear.HasValue)
                laptop.ReleaseYear = dto.ReleaseYear.Value;

            if (dto.IsActive.HasValue)
                laptop.IsActive = dto.IsActive.Value;
        }

        private static MainLaptopUpdatedViewModel MapToViewModel(Laptop laptop)
        {
            return new MainLaptopUpdatedViewModel
            {
                Id = laptop.Id,
                ModelName = laptop.ModelName,
                Brand = new BrandViewModel
                {
                    Id = laptop.Brand.Id,
                    Name = laptop.Brand.Name
                },
                Category = new CategoryViewModel
                {
                    Id = laptop.Category.Id,
                    Name = laptop.Category.Name
                },
                Processor = laptop.Processor,
                GPU = laptop.GPU,
                ScreenSize = laptop.ScreenSize,
                HasCamera = laptop.HasCamera,
                HasKeyboard = laptop.HasKeyboard,
                HasTouchScreen = laptop.HasTouchScreen,
                Description = laptop.Description,
                ReleaseYear = laptop.ReleaseYear,
                StoreLocation = laptop.StoreLocation,
                StoreContact = laptop.StoreContact,
                IsActive = laptop.IsActive,
                CreatedAt = laptop.CreatedAt,
                UpdatedAt = laptop.UpdatedAt ?? laptop.CreatedAt
            };
        }
    }
}