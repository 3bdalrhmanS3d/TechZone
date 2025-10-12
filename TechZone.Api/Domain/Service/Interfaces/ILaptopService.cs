using TechZone.Domain.DTOs.Laptop;
using TechZone.Domain.DTOs.Laptop;
using TechZone.Domain.Entities;
using TechZone.Domain.ENUMS.Laptop;
using TechZone.Domain.PagedResult;
using TechZone.Domain.ServiceResponse;

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
        Task<ServiceResponse<IEnumerable<Laptop>>> GetBySpecificationsAsync(string? processor = null, string? gpu = null, int? minPrice = null, int? maxPrice = null);
    }
}
