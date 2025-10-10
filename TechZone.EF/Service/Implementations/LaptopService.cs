using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TechZone.Api.DTOs.Laptop;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.DTOs.Laptop;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;
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

        public LaptopService(IUnitOfWork unitOfWork, ILogger<LaptopService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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

                var laptop = await _unitOfWork.Laptops.GetByIdAsync(labId, l => l.Variants);
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

                // Map DTO -> Model
                var laptop = new Laptop
                {
                    ModelName = dto.ModelName,
                    Processor = dto.Processor,
                    GPU = dto.GPU,
                    ScreenSize = dto.ScreenSize,
                    HasCamera = dto.HasCamera,
                    HasKeyboard = dto.HasKeyboard,
                    HasTouchScreen = dto.HasTouchScreen,
                    Ports = dto.Ports
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
                existingLaptop.Ports = dto.Ports;

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

                _unitOfWork.Laptops.Delete(laptop);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Laptop with ID {LaptopId} deleted successfully", id);

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

                var laptops = await _unitOfWork.Laptops.WhereAsync(
                    l => l.ModelName.Contains(searchTerm) ||
                         l.Processor.Contains(searchTerm) ||
                         l.GPU.Contains(searchTerm),
                    includes: l => l.Variants
                );

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
            int? minPrice = null,
            int? maxPrice = null)
        {
            try
            {
                _logger.LogInformation("Filtering laptops by specifications");

                var laptops = await _unitOfWork.Laptops.GetAllAsync(l => l.Variants);

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
                            (!minPrice.HasValue || v.Price >= minPrice.Value) &&
                            (!maxPrice.HasValue || v.Price <= maxPrice.Value)
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
    }
}