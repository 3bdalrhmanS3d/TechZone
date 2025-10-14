using MediatR;
using TechZoneV1.Features.Laptops.GetAllLaptops.Queries;
using TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Endpoints
{
    public static class GetAllLaptopEndpoint
    {
        public static void MapGetAllLaptopEndpoint(this WebApplication app)
        {
            app.MapGet("/api/test/laptops", async (IMediator mediator, CancellationToken cancellationToken) =>
            {
                var query = new GetAllLaptopsQuery();
                var laptops = await mediator.Send(query, cancellationToken);
                return Results.Ok(laptops);
            })
            .WithName("GetAllLaptops")
            .WithTags("Laptops")
            .Produces<List<LaptopResponseViewModel>>(StatusCodes.Status200OK);
        }
    }
}
