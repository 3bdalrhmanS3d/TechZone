using TechZone.Api.DTOs.Laptop;
using TechZone.Core.DTOs.Laptop;
using TechZone.Core.Entities;
using TechZone.Core.ENUMS.Laptop;
using TechZone.Core.PagedResult;
using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Services.Interfaces
{
    public interface ILaptopService
    {
        Task<ServiceResponse<PagedResult<LaptopResponseDTO>>> GetAllAsync(PaginationParamsDto<LaptopSortBy> paginationParams);
        Task<ServiceResponse<Laptop>> GetByIdAsync(int id);
        Task<ServiceResponse<Laptop>> CreateAsync(CreateLaptopDto laptop);
        Task<ServiceResponse<Laptop>> UpdateAsync(int id, UpdateLaptopDto laptop);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
        Task<ServiceResponse<IEnumerable<Laptop>>> SearchAsync(string searchTerm);
        Task<ServiceResponse<IEnumerable<Laptop>>> GetBySpecificationsAsync(string? processor = null, string? gpu = null, decimal? minPrice = null, decimal? maxPrice = null);

        // Fixed return types - these should return ServiceResponse
        Task<ServiceResponse<IEnumerable<Laptop>>> GetFeaturedLaptopsAsync(int count = 10);
    }
}