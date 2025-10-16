using MediatR;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Profile.EditUserProfile.Dtos;

namespace TechZoneV1.Features.Profile.EditUserProfile.Commands
{
    public record EditProfileCommand(EditProfileDto ProfileDto):IRequest<ServiceResponse<bool>>;
}
