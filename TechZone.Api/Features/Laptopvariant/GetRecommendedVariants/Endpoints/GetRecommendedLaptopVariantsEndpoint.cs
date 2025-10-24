using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Queries;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Endpoints
{
    public static class GetRecommendedLaptopVariantsEndpoint
    {
        public static void MapGetRecommendedLaptopVariantsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptop-variants/recommended", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromQuery] RecommendedLaptopVariantSortBy? sortBy = null,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 20,
                [FromQuery] string? search = null,
                [FromQuery] SortDirection sortDirection = SortDirection.Asc,
                [FromQuery] int? categoryId = null,
                [FromQuery] int? brandId = null
                ) =>
            {
                var queryDto = new RecommendedLaptopVariantsQueryDto
                {
                    Page = page,
                    PageSize = pageSize,
                    Search = search,
                    SortBy = sortBy ?? RecommendedLaptopVariantSortBy.Name, // Provide default here
                    SortDirection = sortDirection,
                    CategoryId = categoryId,
                    BrandId = brandId
                };

                var query = new GetRecommendedLaptopVariantsQuery(queryDto);
                var result = await mediator.Send(query, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Results.NotFound(EndpointResponse<PagedResult<RecommendedLaptopVariantViewModel>>.NotFoundResponse(
                        result.Message,
                        result.MessageAr
                    ));
                }

                return Results.Ok(EndpointResponse<PagedResult<RecommendedLaptopVariantViewModel>>.SuccessResponse(
                    data: result.Data!,
                    message: result.Message,
                    messageAr: result.MessageAr
                ));
            })
            .WithName("GetRecommendedLaptopVariants")
            .WithTags("Laptop Variants");
            
        }
    }
}

