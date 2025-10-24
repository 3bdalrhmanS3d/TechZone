using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptops.GetAllLaptops.Dtos;
using TechZoneV1.Features.Laptops.GetAllLaptops.Queries;
using TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Endpoints
{
    public static class GetAllLaptopsEndpoint
    {
        public static void MapGetAllLaptopsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptops", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 20,
                [FromQuery] string? search = "",
                [FromQuery] SortDirection sortDirection = SortDirection.Asc,
                [FromQuery] FullLaptopSortBy? sortBy = null,
                [FromQuery] int? brandId = null,
                [FromQuery] int? categoryId = null,
                [FromQuery] bool? isActive = null,
                [FromQuery] int? releaseYear = null,
                [FromQuery] bool? hasCamera = null,
                [FromQuery] bool? hasTouchScreen = null) =>
            {
                // Provide default value if sortBy is null
                var sortByValue = sortBy ?? FullLaptopSortBy.AverageRating;

                var queryDto = new LaptopQueryDto
                {
                    Page = page,
                    PageSize = pageSize,
                    Search = search,
                    SortBy = sortByValue,
                    SortDirection = sortDirection,
                    BrandId = brandId,
                    CategoryId = categoryId,
                    IsActive = isActive,
                    ReleaseYear = releaseYear,
                    HasCamera = hasCamera,
                    HasTouchScreen = hasTouchScreen
                };

                var query = new GetAllLaptopsQuery(queryDto);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.NotFound(EndpointResponse<PagedResult<LaptopResponseViewModel>>.NotFoundResponse(
                        result.Message,
                        result.MessageAr
                    ));
                }

                return Results.Ok(EndpointResponse<PagedResult<LaptopResponseViewModel>>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetAllLaptops")
            .WithTags("Laptops");
        }
    }
}