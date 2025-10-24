using MediatR;
using TechZoneV1.Features.Laptops.DeleteLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.DeleteLaptop.Commands
{
    public class DeleteLaptopCommand : IRequest<RequestResponse<LaptopDeletedViewModel>>
    {
        public int Id { get; }

        public DeleteLaptopCommand(int id)
        {
            Id = id;
        }
    }
}