using MediatR;
using TechZoneV1.Features.LaptopVariant.UpdateStock.Dtos;
using TechZoneV1.Features.LaptopVariant.UpdateStock.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.UpdateStock.Commands
{
    public class UpdateStockCommand : IRequest<RequestResponse<StockUpdateViewModel>>
    {
        public int Id { get; }
        public UpdateStockDto UpdateDto { get; }

        public UpdateStockCommand(int id, UpdateStockDto updateDto)
        {
            Id = id;
            UpdateDto = updateDto;
        }
    }
}