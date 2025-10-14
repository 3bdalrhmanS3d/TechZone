using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Features.Profile.Endpoints
{
    public static class GetProfileEndpoint
    {
        public static void MapProfileEndpoint(this WebApplication app)
        {
            app.MapGet("/api/profile", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new Queries.GetProfileQuery());
                return result;
            })
                .RequireAuthorization()
                .WithName("get")
                .WithTags("Profile");
        }
    }
}
