using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Laptops.CreateLaptop.Commands;
using TechZoneV1.Features.Laptops.CreateLaptop.Dtos;
using TechZoneV1.Features.Laptops.CreateLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.CreateLaptop.Handlers
{
    public class CreateLaptopCommandHandler : IRequestHandler<CreateLaptopCommand, RequestResponse<LaptopCreatedViewModel>>
    {
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IBaseRepository<Brand> _brandRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLaptopCommandHandler(
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

        public async Task<RequestResponse<LaptopCreatedViewModel>> Handle(
            CreateLaptopCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var createLaptopDto = request.CreateLaptopDto;

                // Validate brand exists
                var brand = await _brandRepository.GetAll()
                    .Where(b => b.Id == createLaptopDto.BrandId && b.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);

                if (brand == null)
                {
                    return RequestResponse<LaptopCreatedViewModel>.Fail(
                        "Brand not found or inactive",
                        "العلامة التجارية غير موجودة أو غير مفعلة"
                    );
                }

                // Validate category exists
                var category = await _categoryRepository.GetAll()
                    .Where(c => c.Id == createLaptopDto.CategoryId && c.IsActive)
                    .FirstOrDefaultAsync(cancellationToken);

                if (category == null)
                {
                    return RequestResponse<LaptopCreatedViewModel>.Fail(
                        "Category not found or inactive",
                        "الفئة غير موجودة أو غير مفعلة"
                    );
                }

                // Check if laptop with same model name already exists (optional)
                var existingLaptop = await _laptopRepository.GetAll()
                    .Where(l => l.ModelName == createLaptopDto.ModelName && !l.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingLaptop != null)
                {
                    return RequestResponse<LaptopCreatedViewModel>.Fail(
                        "A laptop with this model name already exists",
                        "يوجد لابتوب بنفس اسم الموديل مسبقاً"
                    );
                }

                // Create new laptop entity
                var laptop = new Laptop
                {
                    ModelName = createLaptopDto.ModelName,
                    BrandId = createLaptopDto.BrandId,
                    CategoryId = createLaptopDto.CategoryId,
                    Processor = createLaptopDto.Processor,
                    GPU = createLaptopDto.GPU ?? string.Empty,
                    ScreenSize = createLaptopDto.ScreenSize ?? string.Empty,
                    HasCamera = createLaptopDto.HasCamera,
                    HasKeyboard = createLaptopDto.HasKeyboard,
                    HasTouchScreen = createLaptopDto.HasTouchScreen,
                    Description = createLaptopDto.Description ?? string.Empty,
                    StoreLocation = createLaptopDto.StoreLocation ?? string.Empty,
                    StoreContact = createLaptopDto.StoreContact ?? string.Empty,
                    ReleaseYear = createLaptopDto.ReleaseYear,
                    IsActive = createLaptopDto.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Add to repository
                await _laptopRepository.AddAsync(laptop);
                await _unitOfWork.SaveChangesAsync();

                // Map to ViewModel
                var response = MapToViewModel(laptop, brand, category);

                return RequestResponse<LaptopCreatedViewModel>.Success(
                    response,
                    "Laptop created successfully",
                    "تم إنشاء اللابتوب بنجاح"
                );
            }
            catch (DbUpdateException ex)
            {
                // Log database exception
                return RequestResponse<LaptopCreatedViewModel>.Fail(
                    "An error occurred while saving laptop data",
                    "حدث خطأ أثناء حفظ بيانات اللابتوب"
                );
            }
            catch (Exception ex)
            {
                // Log general exception
                return RequestResponse<LaptopCreatedViewModel>.Fail(
                    "An error occurred while creating laptop",
                    "حدث خطأ أثناء إنشاء اللابتوب"
                );
            }
        }

        private static LaptopCreatedViewModel MapToViewModel(Laptop laptop, Brand brand, TechZone.Domain.Entities.Category category)
        {
            return new LaptopCreatedViewModel
            {
                Id = laptop.Id,
                ModelName = laptop.ModelName,
                Brand = new BrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name
                },
                Category = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name
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