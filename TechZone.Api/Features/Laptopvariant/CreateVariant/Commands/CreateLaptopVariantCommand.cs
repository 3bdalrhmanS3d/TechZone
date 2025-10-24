using MediatR;
using TechZoneV1.Features.LaptopVariant.CreateVariant.Dtos;
using TechZoneV1.Features.LaptopVariant.CreateVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.CreateVariant.Commands
{
    public class CreateLaptopVariantCommand : IRequest<RequestResponse<LaptopVariantViewModel>>
    {
        public CreateLaptopVariantDto CreateDto { get; }

        public CreateLaptopVariantCommand(CreateLaptopVariantDto createDto)
        {
            CreateDto = createDto;
        }
    }
}