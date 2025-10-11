using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TechZone.Api.DTOs.Laptop;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.DTOs.Laptop;
using TechZone.Core.Entities;
using TechZone.Core.ENUMS.Laptop;
using TechZone.Core.Interfaces;
using TechZone.Core.PagedResult;
using TechZone.Core.ServiceResponse;

namespace TechZone.EF.Service.Implementations
{
    public class LaptopService : ILaptopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LaptopService> _logger;
        private readonly IBaseRepository<LaptopWarranty> _laptopWarranty;
        private readonly IBaseRepository<LaptopPort> _laptopPorts; 


        public LaptopService(IUnitOfWork unitOfWork, ILogger<LaptopService> logger,
            IBaseRepository<LaptopPort> laptopPorts , 
            IBaseRepository<LaptopWarranty> laptopWarranty
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _laptopPorts = laptopPorts;
            _laptopWarranty = laptopWarranty;
        }
        public async Task<ServiceResponse<IEnumerable<FullLaptopResponseDTO>>> GetAllFullAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all laptops with full details");

                var laptops = await _unitOfWork.Laptops.GetAllAsync(
                    l => l.Brand,
                    l => l.Category,
                    l => l.Images,
                    l => l.Variants
                );

                if (laptops == null || !laptops.Any())
                {
                    _logger.LogWarning("No laptops found in the database");
                    return ServiceResponse<IEnumerable<FullLaptopResponseDTO>>.NotFoundResponse(
                        "No laptops found",
                        "لم يتم العثور على أجهزة كمبيوتر محمولة"
                    );
                }


                var laptopDtos = laptops.Select( l => new FullLaptopResponseDTO
                {
                    Id = l.Id,
                    ModelName = l.ModelName,
                    Processor = l.Processor,
                    GPU = l.GPU,
                    ScreenSize = l.ScreenSize,
                    HasCamera = l.HasCamera,
                    HasKeyboard = l.HasKeyboard,
                    HasTouchScreen = l.HasTouchScreen,
                    Ports = _laptopPorts.GetAll().Where(lp => lp.LaptopId == l.Id).ToList(),
                    Warranty =_laptopWarranty.GetAll().Where(lw => lw.LaptopId == l.Id).ToList(),
                    Description = l.Description,
                    //Notes = l.Notes,

                    Brand = new BrandDTO
                    {
                        Id = l.Brand.Id,
                        Name = l.Brand.Name
                    },

                    Category = new CategoryDTO
                    {
                        Id = l.Category.Id,
                        Name = l.Category.Name
                    },

                    Variants = l.Variants.Select(v => new LaptopVariantDTO
                    {
                        Id = v.Id,
                        Ram = v.RAM,
                        Storage = v.StorageCapacityGB,
                        Price = v.CurrentPrice,
                        StockQuantity = v.StockQuantity
                    }).ToList(),

                    Images = l.Images.Select(i => new LaptopImageDTO
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        IsMain = i.IsMain
                    }).ToList()
                }).ToList();

                return ServiceResponse<IEnumerable<FullLaptopResponseDTO>>.SuccessResponse(
                    laptopDtos,
                    "All laptops retrieved successfully",
                    "تم استرجاع جميع أجهزة الكمبيوتر المحمولة بنجاح"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all laptops with full details");
                return ServiceResponse<IEnumerable<FullLaptopResponseDTO>>.InternalServerErrorResponse(
                    "An error occurred while retrieving laptops",
                    "حدث خطأ أثناء استرجاع أجهزة الكمبيوتر المحمولة"
                );
            }
        }

        public async Task<ServiceResponse<PagedResult<LaptopResponseDTO>>> GetAllAsync(PaginationParamsDto<LaptopSortBy> paginationParams)
        {
            try
            {
                _logger.LogInformation("Retrieving laptops with parameters: {@PaginationParams}", paginationParams);
                var result = await _unitOfWork.Laptops.GetPagedAsync(paginationParams);

                return ServiceResponse<PagedResult<LaptopResponseDTO>>.SuccessResponse(
                    result,
                    "Laptops retrieved successfully",
                    "تم استرجاع أجهزة الكمبيوتر المحمولة بنجاح"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving laptops");
                return ServiceResponse<PagedResult<LaptopResponseDTO>>.InternalServerErrorResponse(
                    "An error occurred while retrieving laptops"
                );
            }
        }

        public async Task<ServiceResponse<Laptop>> GetByIdAsync(int labId)
        {
            try
            {
                _logger.LogInformation("Retrieving laptop with ID: {LaptopId}", labId);

                if (labId <= 0)
                {
                    return ServiceResponse<Laptop>.ErrorResponse(
                        "Invalid laptop ID. ID must be greater than 0",
                        "معرف الكمبيوتر المحمول غير صالح. يجب أن يكون المعرف أكبر من 0",
                        400
                    );
                }

                var laptop = await _unitOfWork.Laptops.GetByIdAsync(labId);
                if (laptop == null)
                {
                    _logger.LogWarning("Laptop with ID: {LaptopId} not found", labId);
                    return ServiceResponse<Laptop>.NotFoundResponse(
                                $"Laptop with ID {labId} not found",
                                $"الكمبيوتر المحمول بالمعرف {labId} غير موجود");
                }

                return ServiceResponse<Laptop>.SuccessResponse(
                    laptop,
                    "Laptop retrieved successfully",
                    "تم استرجاع الكمبيوتر المحمول بنجاح"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving laptop with ID: {LaptopId}", labId);
                return ServiceResponse<Laptop>.InternalServerErrorResponse(
                    "An error occurred while retrieving the laptop",
                    "حدث خطأ أثناء استرجاع الكمبيوتر المحمول"
                );
            }
        }

        public async Task<ServiceResponse<Laptop>> CreateAsync(CreateLaptopDto dto)
        {
            try
            {
                _logger.LogInformation("Creating new laptop: {ModelName}", dto.ModelName);

                // Check if laptop with same model already exists
                var existingLaptop = await _unitOfWork.Laptops.FirstOrDefaultAsync(
                    l => l.ModelName.ToLower() == dto.ModelName.ToLower()
                );

                if (existingLaptop != null)
                {
                    return ServiceResponse<Laptop>.ConflictResponse(
                        $"Laptop with model name '{dto.ModelName}' already exists"
                    );
                }

                // Map DTO -> Model with new schema properties
                var laptop = new Laptop
                {
                    ModelName = dto.ModelName,
                    Processor = dto.Processor,
                    GPU = dto.GPU,
                    ScreenSize = dto.ScreenSize,
                    HasCamera = dto.HasCamera,
                    HasKeyboard = dto.HasKeyboard,
                    HasTouchScreen = dto.HasTouchScreen,
                    Description = dto.Description ?? string.Empty,
                    StoreLocation = dto.StoreLocation ?? string.Empty,
                    StoreContact = dto.StoreContact ?? string.Empty,
                    ReleaseYear = dto.ReleaseYear,
                    BrandId = dto.BrandId ?? 0,
                    CategoryId = dto.CategoryId??0,
                    IsActive = true
                };

                await _unitOfWork.Laptops.AddAsync(laptop);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Laptop created successfully with ID: {LaptopId}", laptop.Id);

                return ServiceResponse<Laptop>.SuccessResponse(
                    laptop,
                    "Laptop created successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating laptop");
                return ServiceResponse<Laptop>.InternalServerErrorResponse(
                    "An error occurred while creating the laptop"
                );
            }
        }

        public async Task<ServiceResponse<Laptop>> UpdateAsync(int id, UpdateLaptopDto dto)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<Laptop>.ErrorResponse(
                        "Invalid laptop ID. ID must be greater than 0",
                        "معرف الكمبيوتر المحمول غير صالح. يجب أن يكون المعرف أكبر من 0",
                        400
                    );
                }

                _logger.LogInformation("Updating laptop with ID: {LaptopId}", id);

                var existingLaptop = await _unitOfWork.Laptops.GetByIdAsync(id);
                if (existingLaptop == null)
                {
                    _logger.LogWarning("Laptop with ID {LaptopId} not found for update", id);
                    return ServiceResponse<Laptop>.NotFoundResponse(
                        $"Laptop with ID {id} not found",
                        $"الكمبيوتر المحمول بالمعرف {id} غير موجود"
                    );
                }

                // Check if model name conflicts with another laptop
                var conflictingLaptop = await _unitOfWork.Laptops.FirstOrDefaultAsync(
                    l => l.ModelName.ToLower() == dto.ModelName.ToLower() && l.Id != id
                );

                if (conflictingLaptop != null)
                {
                    return ServiceResponse<Laptop>.ConflictResponse(
                        $"Another laptop with model name '{dto.ModelName}' already exists"
                    );
                }

                // Map DTO -> Model (update properties)
                existingLaptop.ModelName = dto.ModelName;
                existingLaptop.Processor = dto.Processor;
                existingLaptop.GPU = dto.GPU;
                existingLaptop.ScreenSize = dto.ScreenSize;
                existingLaptop.HasCamera = dto.HasCamera;
                existingLaptop.HasKeyboard = dto.HasKeyboard;
                existingLaptop.HasTouchScreen = dto.HasTouchScreen;
                existingLaptop.Description = dto.Description ?? existingLaptop.Description;
                existingLaptop.StoreLocation = dto.StoreLocation ?? existingLaptop.StoreLocation;
                existingLaptop.StoreContact = dto.StoreContact ?? existingLaptop.StoreContact;
                existingLaptop.ReleaseYear = dto.ReleaseYear ?? existingLaptop.ReleaseYear;
                existingLaptop.BrandId = dto.BrandId ?? existingLaptop.BrandId;
                existingLaptop.CategoryId = dto.CategoryId ?? existingLaptop.CategoryId;
                existingLaptop.IsActive = dto.IsActive ?? existingLaptop.IsActive;
                existingLaptop.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Laptops.Update(existingLaptop);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Laptop with ID {LaptopId} updated successfully", id);

                return ServiceResponse<Laptop>.SuccessResponse(
                    existingLaptop,
                    "Laptop updated successfully",
                    "تم تحديث الكمبيوتر المحمول بنجاح"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating laptop with ID: {LaptopId}", id);
                return ServiceResponse<Laptop>.InternalServerErrorResponse(
                    "An error occurred while updating the laptop",
                    "حدث خطأ أثناء تحديث الكمبيوتر المحمول"
                );
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        "Invalid laptop ID. ID must be greater than 0",
                        "معرف الكمبيوتر المحمول غير صالح. يجب أن يكون المعرف أكبر من 0",
                        400
                    );
                }

                _logger.LogInformation("Deleting laptop with ID: {LaptopId}", id);

                var laptop = await _unitOfWork.Laptops.GetByIdAsync(id);
                if (laptop == null)
                {
                    _logger.LogWarning("Laptop with ID {LaptopId} not found for deletion", id);
                    return ServiceResponse<bool>.NotFoundResponse(
                        $"Laptop with ID {id} not found"
                    );
                }

                // Soft delete by setting IsActive to false and DeletedAt timestamp
                laptop.IsActive = false;
                laptop.DeletedAt = DateTime.UtcNow;

                _unitOfWork.Laptops.Update(laptop);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Laptop with ID {LaptopId} soft deleted successfully", id);

                return ServiceResponse<bool>.SuccessResponse(
                    true,
                    "Laptop deleted successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting laptop with ID: {LaptopId}", id);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while deleting the laptop"
                );
            }
        }

        public async Task<ServiceResponse<IEnumerable<Laptop>>> SearchAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return ServiceResponse<IEnumerable<Laptop>>.ErrorResponse(
                        "Search term cannot be empty",
                        "لا يمكن أن يكون مصطلح البحث فارغًا",
                        400
                    );
                }

                _logger.LogInformation("Searching laptops with term: {SearchTerm}", searchTerm);

                // Use GetAllAsync with specific includes
                var allLaptops = await _unitOfWork.Laptops.GetAllAsync(
                    l => l.Variants,
                    l => l.Brand,
                    l => l.Category
                );

                // Apply search filter in memory (for simple scenarios) or use a different approach
                var laptops = allLaptops.Where(l =>
                    l.ModelName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    l.Processor.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    l.GPU.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (l.Brand != null && l.Brand.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();

                return ServiceResponse<IEnumerable<Laptop>>.SuccessResponse(
                    laptops,
                    $"Found {laptops.Count()} laptops matching '{searchTerm}'"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching laptops with term: {SearchTerm}", searchTerm);
                return ServiceResponse<IEnumerable<Laptop>>.InternalServerErrorResponse(
                    "An error occurred while searching laptops"
                );
            }
        }
        public async Task<ServiceResponse<IEnumerable<Laptop>>> GetBySpecificationsAsync(
            string? processor = null,
            string? gpu = null,
            decimal? minPrice = null,
            decimal? maxPrice = null)
        {
            try
            {
                _logger.LogInformation("Filtering laptops by specifications");

                var laptops = await _unitOfWork.Laptops.GetAllAsync(
                    l => l.Variants,
                    l => l.Brand,
                    l => l.Category
                );

                var filteredLaptops = laptops.AsQueryable();

                if (!string.IsNullOrWhiteSpace(processor))
                {
                    filteredLaptops = filteredLaptops.Where(l =>
                        l.Processor.Contains(processor, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(gpu))
                {
                    filteredLaptops = filteredLaptops.Where(l =>
                        l.GPU.Contains(gpu, StringComparison.OrdinalIgnoreCase));
                }

                if (minPrice.HasValue || maxPrice.HasValue)
                {
                    filteredLaptops = filteredLaptops.Where(l =>
                        l.Variants.Any(v =>
                            (!minPrice.HasValue || v.CurrentPrice >= minPrice.Value) && // Updated from Price to CurrentPrice
                            (!maxPrice.HasValue || v.CurrentPrice <= maxPrice.Value)    // Updated from Price to CurrentPrice
                        ));
                }

                var result = filteredLaptops.ToList();

                return ServiceResponse<IEnumerable<Laptop>>.SuccessResponse(
                    result,
                    $"Found {result.Count} laptops matching the specified criteria"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while filtering laptops by specifications");
                return ServiceResponse<IEnumerable<Laptop>>.InternalServerErrorResponse(
                    "An error occurred while filtering laptops"
                );
            }
        }

        // New method to get featured laptops
        public async Task<ServiceResponse<IEnumerable<Laptop>>> GetFeaturedLaptopsAsync(int count = 10)
        {
            try
            {
                _logger.LogInformation("Retrieving {Count} featured laptops", count);

                var laptops = await _unitOfWork.Laptops.GetFeaturedLaptopsAsync(count);

                return ServiceResponse<IEnumerable<Laptop>>.SuccessResponse(
                    laptops,
                    "Featured laptops retrieved successfully",
                    "تم استرجاع أجهزة الكمبيوتر المحمولة المميزة بنجاح"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving featured laptops");
                return ServiceResponse<IEnumerable<Laptop>>.InternalServerErrorResponse(
                    "An error occurred while retrieving featured laptops"
                );
            }
        }


    }
}