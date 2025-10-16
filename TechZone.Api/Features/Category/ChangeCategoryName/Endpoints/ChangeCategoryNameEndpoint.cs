using MediatR;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Category.ChangeCategoryName.Commands;
using TechZoneV1.Features.Category.ChangeCategoryName.Dtos;

namespace TechZoneV1.Features.Category.ChangeCategoryName.Endpoints
{
    public static class ChangeCategoryNameEndpoint
    {
        public static void MapChangeCategoryNameEndpoint(this WebApplication app)
        {
            app.MapPut("/api/categories/change-name", async (ChangeCategoryNameRequestDto request, IMediator mediator) =>
            {
                var result = await mediator.Send(new ChangeCategoryNameCommand(request));
                return Results.Ok(result);
            })
            .WithName("ChangeCategoryName")
            .WithTags("Categories")
            .Produces<ServiceResponse<ChangeCategoryNameResponseDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }   
    }
}
