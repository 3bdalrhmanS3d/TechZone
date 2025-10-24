using MediatR;
using TechZoneV1.Features.LaptopVariant.GetVariantById.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantById.Queries
{
    public class GetLaptopVariantByIdQuery : IRequest<RequestResponse<LaptopVariantDetailsViewModel>>
    {
        public int Id { get; }

        public GetLaptopVariantByIdQuery(int id)
        {
            Id = id;
        }
    }
}