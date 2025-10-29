using MediatR;
using TechZoneV1.Features.LaptopVariant.GetVariantById.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.GetVariantById.Queries
{
    public record GetLaptopVariantByIdQuery (int Id) : IRequest<RequestResponse<LaptopVariantDetailsViewModel>>;

}