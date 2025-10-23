using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechZone.Domain.PagedResult;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.Dtos;
using TechZoneV1.Features.Laptopvariant.GetRecommendedVariants.ViewModels;
using TechZoneV1.Features.LaptopVariant.FilterVariants.Dtos;
using TechZoneV1.Features.LaptopVariant.FilterVariants.Queries;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.FilterVariants.Endpoints
{
    public static class FilterLaptopVariantsEndpoint
    {
        public static void MapFilterLaptopVariantsEndpoint(this WebApplication app)
        {
            app.MapGet("/api/laptop-variants/filter", async (
                IMediator mediator,
                CancellationToken cancellationToken,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 20,
                [FromQuery] string? search = null,
                [FromQuery] RecommendedLaptopVariantSortBy? sortBy = null,
                [FromQuery] SortDirection sortDirection = SortDirection.Asc,
                [FromQuery] string? brandIds = null,
                [FromQuery] string? categoryIds = null,
                [FromQuery] decimal? minPrice = null,
                [FromQuery] decimal? maxPrice = null,
                [FromQuery] string? processor = null,
                [FromQuery] string? ram = null,
                [FromQuery] string? storage = null,
                [FromQuery] string? storageType = null,
                [FromQuery] string? screenSize = null,
                [FromQuery] bool? hasCamera = null,
                [FromQuery] bool? hasTouchScreen = null,
                [FromQuery] bool? inStock = null,
                [FromQuery] bool? hasDiscount = null,
                [FromQuery] decimal? minRating = null,
                [FromQuery] int? releaseYear = null
                ) =>
            {
                // Parse array parameters from comma-separated strings
                var brandIdsList = ParseIntList(brandIds);
                var categoryIdsList = ParseIntList(categoryIds);
                var ramList = ParseIntList(ram);
                var storageList = ParseIntList(storage);
                var screenSizeList = ParseStringList(screenSize);

                var queryDto = new FilterLaptopVariantsQueryDto
                {
                    Page = page,
                    PageSize = pageSize,
                    Search = search,
                    SortBy = sortBy, // Remove the cast - use nullable directly
                    SortDirection = sortDirection,
                    BrandIds = brandIdsList,
                    CategoryIds = categoryIdsList,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    Processor = processor,
                    Ram = ramList,
                    Storage = storageList,
                    StorageType = storageType,
                    ScreenSize = screenSizeList,
                    HasCamera = hasCamera,
                    HasTouchScreen = hasTouchScreen,
                    InStock = inStock,
                    HasDiscount = hasDiscount,
                    MinRating = minRating,
                    ReleaseYear = releaseYear
                };

                var query = new FilterLaptopVariantsQuery(queryDto);
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
            .WithName("FilterLaptopVariants")
            .WithTags("Laptop Variants");
        }

        private static List<int>? ParseIntList(string? input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            return input.Split(',')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => int.TryParse(x.Trim(), out var result) ? result : (int?)null)
                .Where(x => x.HasValue)
                .Select(x => x!.Value)
                .ToList();
        }

        private static List<string>? ParseStringList(string? input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            return input.Split(',')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim())
                .ToList();
        }
    }
}