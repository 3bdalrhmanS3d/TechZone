using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.DTOs.Laptop;
using TechZone.Domain.Entities;
using TechZone.Domain.ENUMS.Laptop;
using TechZone.Domain.PagedResult;

namespace TechZone.Domain.Interfaces
{
    public interface ILaptopRepository : IBaseRepository<Laptop>
    {
        // You can add Laptop-specific queries here later

        Task<PagedResult<LaptopResponseDTO>> GetPagedAsync(PaginationParamsDto<LaptopSortBy> paginationParams);
        Task<int> CountAsync();
    }
}
