using MediatR;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Dtos;
using TechZoneV1.Features.LaptopVariant.BulkUpdateStock.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.BulkUpdateStock.Commands
{
    public class BulkUpdateStockCommand : IRequest<RequestResponse<BulkUpdateStockViewModel>>
    {
        public BulkUpdateStockDto BulkUpdateDto { get; }

        public BulkUpdateStockCommand(BulkUpdateStockDto bulkUpdateDto)
        {
            BulkUpdateDto = bulkUpdateDto;
        }
    }
}