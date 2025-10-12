using TechZone.Domain.DTOs;
using TechZone.Domain.DTOs.Category;
using TechZone.Domain.PagedResult;
using TechZone.Domain.ServiceResponse;

namespace TechZone.Domain.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<PagedResult<CategoryDto>>> GetAllAsync(PaginationParamsDto<CategorySortBy> request, CancellationToken ct = default);
        Task<ServiceResponse<CategoryDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ServiceResponse<CategoryDto>> CreateAsync(CreateCategoryRequest dto, CancellationToken ct = default);
        Task<ServiceResponse<object>> UpdateAsync(int id, UpdateCategoryRequest dto, CancellationToken ct = default);
        Task<ServiceResponse<object>> DeleteAsync(int id, CancellationToken ct = default);
        Task<ServiceResponse<List<CategoryWithCountDto>>> GetWithLaptopCountsAsync(CancellationToken ct = default);
    }
}
