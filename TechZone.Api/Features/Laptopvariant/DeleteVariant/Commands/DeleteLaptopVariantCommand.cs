using MediatR;
using TechZoneV1.Features.LaptopVariant.DeleteVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.DeleteVariant.Commands
{
    public class DeleteLaptopVariantCommand : IRequest<RequestResponse<DeleteLaptopVariantViewModel>>
    {
        public int Id { get; }

        public DeleteLaptopVariantCommand(int id)
        {
            Id = id;
        }
    }
}