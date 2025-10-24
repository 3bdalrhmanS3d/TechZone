using MediatR;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.Dtos;
using TechZoneV1.Features.LaptopVariant.UpdateVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.UpdateVariant.Commands
{
    public class UpdateLaptopVariantCommand : IRequest<RequestResponse<UpdateLaptopVariantViewModel>>
    {
        public int Id { get; }
        public UpdateLaptopVariantDto UpdateDto { get; }

        public UpdateLaptopVariantCommand(int id, UpdateLaptopVariantDto updateDto)
        {
            Id = id;
            UpdateDto = updateDto;
        }
    }
}