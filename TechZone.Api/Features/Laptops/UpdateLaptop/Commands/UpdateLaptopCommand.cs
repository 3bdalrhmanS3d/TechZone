using MediatR;
using TechZoneV1.Features.Laptops.UpdateLaptop.Dtos;
using TechZoneV1.Features.Laptops.UpdateLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.UpdateLaptop.Commands
{
    public class UpdateLaptopCommand : IRequest<RequestResponse<MainLaptopUpdatedViewModel>>
    {
        public int Id { get; }
        public UpdateMainLaptopDto UpdateLaptopDto { get; }

        public UpdateLaptopCommand(int id, UpdateMainLaptopDto  updateLaptopDto)
        {
            Id = id;
            UpdateLaptopDto = updateLaptopDto;
        }
    }
}