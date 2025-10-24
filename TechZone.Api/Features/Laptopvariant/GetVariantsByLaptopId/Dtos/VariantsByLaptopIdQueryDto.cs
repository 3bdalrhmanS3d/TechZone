using System.ComponentModel.DataAnnotations;
using TechZone.Domain.PagedResult;

namespace TechZoneV1.Features.LaptopVariant.GetVariantsByLaptopId.Dtos
{
    public class VariantsByLaptopIdQueryDto
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        public bool InStockOnly { get; set; } = false;
    }
}