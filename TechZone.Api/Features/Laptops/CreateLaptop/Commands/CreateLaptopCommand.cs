using MediatR;
using TechZoneV1.Features.Laptops.CreateLaptop.Dtos;
using TechZoneV1.Features.Laptops.CreateLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.CreateLaptop.Commands
{
    public class CreateLaptopCommand : IRequest<RequestResponse<LaptopCreatedViewModel>>
    {
        public CreateFullLaptopDto CreateLaptopDto { get; }

        public CreateLaptopCommand(CreateFullLaptopDto createLaptopDto)
        {
            CreateLaptopDto = createLaptopDto;
        }
    }
}