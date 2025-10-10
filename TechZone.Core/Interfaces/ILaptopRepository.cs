using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.DTOs.Laptop;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.ENUMS.Laptop;
using TechZone.Core.PagedResult;

namespace TechZone.Core.Interfaces
{
    public interface ILaptopRepository : IBaseRepository<Laptop>
    {
        // You can add Laptop-specific queries here later

        Task<PagedResult<LaptopResponseDTO>> GetPagedAsync(PaginationParamsDto<LaptopSortBy> paginationParams);
        Task<int> CountAsync();
    }
}
