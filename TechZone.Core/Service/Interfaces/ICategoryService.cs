using TechZone.Core.DTOs;
using TechZone.Core.DTOs.Category;
using TechZone.Core.PagedResult;
using TechZone.Core.ServiceResponse;

namespace TechZone.Core.Service.Interfaces
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
