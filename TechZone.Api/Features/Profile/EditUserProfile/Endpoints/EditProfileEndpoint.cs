using MediatR;
using TechZoneV1.Features.Profile.EditUserProfile.Commands;
using TechZoneV1.Features.Profile.EditUserProfile.Dtos;

namespace TechZoneV1.Features.Profile.EditUserProfile.Endpoints
{
    public static class EditProfileEndpoint
    {
        public static void MapEditEndpoint(this WebApplication app)
        {
            app.MapPut("/api/profile", async (EditProfileDto dto, IMediator mediator) =>
            {
                var result = await mediator.Send(new EditProfileCommand(dto));
                return Results.Ok(result);
            })
            .WithTags("Profile")
            .WithName("EditUserProfile")
            .WithSummary("Edit the profile of the currently authenticated user.")
            .WithDescription("Allows the currently authenticated user to update their profile information such as name, email, and phone number.");
        }
    }
}
