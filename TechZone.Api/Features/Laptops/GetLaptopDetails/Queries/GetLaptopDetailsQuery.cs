using MediatR;
using TechZoneV1.Features.Laptops.GetLaptopDetails.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetLaptopDetails.Queries
{
    public class GetLaptopDetailsQuery : IRequest<RequestResponse<LaptopDetailsResponseViewModel>>
    {
        public int Id { get; }

        public GetLaptopDetailsQuery(int id)
        {
            Id = id;
        }
    }
}