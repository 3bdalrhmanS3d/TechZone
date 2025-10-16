using MediatR;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Category.ChangeCategoryName.Dtos;

namespace TechZoneV1.Features.Category.ChangeCategoryName.Commands
{
    public record ChangeCategoryNameCommand(ChangeCategoryNameRequestDto RequestDto) : IRequest<ServiceResponse<ChangeCategoryNameResponseDto>>;

}
