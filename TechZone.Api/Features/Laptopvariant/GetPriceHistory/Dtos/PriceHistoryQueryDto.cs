using System.ComponentModel.DataAnnotations;

namespace TechZoneV1.Features.LaptopVariant.GetPriceHistory.Dtos
{
    public class PriceHistoryQueryDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
        public int Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
        public int PageSize { get; set; } = 10;

        [Range(1, 3650, ErrorMessage = "Days must be between 1 and 3650")]
        public int? Days { get; set; }
    }
}