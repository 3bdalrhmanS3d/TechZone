using TechZone.Core.DTOs.Brand;
using TechZone.Core.PagedResult;
using TechZone.Core.ServiceResponse;

namespace TechZone.Core.Service.Interfaces
{
    public interface IBrandService
    {
        Task<ServiceResponse<PagedResult<BrandDto>>> GetAllAsync(PaginationParamsDto<BrandSortBy> request, CancellationToken ct = default);
        Task<ServiceResponse<BrandDto>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ServiceResponse<BrandDto>> CreateAsync(CreateBrandRequest dto, CancellationToken ct = default);
        Task<ServiceResponse<object>> UpdateAsync(int id, UpdateBrandRequest dto, CancellationToken ct = default);
        Task<ServiceResponse<object>> DeleteAsync(int id, CancellationToken ct = default);
        Task<ServiceResponse<List<BrandWithCountDto>>> GetWithLaptopCountsAsync(CancellationToken ct = default);
    }
}
