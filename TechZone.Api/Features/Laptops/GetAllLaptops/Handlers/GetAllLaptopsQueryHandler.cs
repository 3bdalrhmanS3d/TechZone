using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZone.Domain.PagedResult;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Laptops.GetAllLaptops.Dtos;
using TechZoneV1.Features.Laptops.GetAllLaptops.Queries;

namespace TechZoneV1.Features.Laptops.GetAllLaptops.Handlers
{
    public class GetAllLaptopsQueryHandler : IRequestHandler<GetAllLaptopsQuery, ServiceResponse<PagedResult<LaptopsDto>>>
    {
        private readonly IBaseRepository<Laptop> _laptopRepository;

        public GetAllLaptopsQueryHandler(IBaseRepository<Laptop> laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }

        public async Task<ServiceResponse<PagedResult<LaptopsDto>>> Handle(GetAllLaptopsQuery request, CancellationToken cancellationToken)
        {
            // Single query with explicit joins and projection
            var laptopsDto = await _laptopRepository.GetAll()
                .Where(l => l.IsActive)
                .Select(l => new LaptopsDto
                {
                    Id = l.Id,
                    ModelName = l.ModelName,
                    Processor = l.Processor,
                    GPU = l.GPU,
                    ScreenSize = l.ScreenSize,
                    HasCamera = l.HasCamera,
                    HasKeyboard = l.HasKeyboard,
                    HasTouchScreen = l.HasTouchScreen,
                    Description = l.Description,
                    Notes = "", // Set as needed

                    // Ports
                    Ports = l.Ports.Select(p => new LaptopPort
                    {
                        Id = p.Id,
                        LaptopId = p.LaptopId,
                        PortType = p.PortType,
                        Quantity = p.Quantity
                    }).ToList(),

                    // Warranties
                    Warranty = l.Warranties.Select(w => new LaptopWarranty
                    {
                        Id = w.Id,
                        LaptopId = w.LaptopId,
                        DurationMonths = w.DurationMonths,
                        Type = w.Type,
                        Coverage = w.Coverage,
                        Provider = w.Provider
                    }).ToList(),

                    // Brand
                    Brand = l.Brand == null ? null : new BrandDTO
                    {
                        Id = l.Brand.Id,
                        Name = l.Brand.Name
                    },

                    // Category
                    Category = l.Category == null ? null : new CategoryDTO
                    {
                        Id = l.Category.Id,
                        Name = l.Category.Name
                    },

                    // Variants
                    Variants = l.Variants
                        .Where(v => v.IsActive)
                        .Select(v => new LaptopVariantDTO
                        {
                            Id = v.Id,
                            Ram = v.RAM,
                            Storage = v.StorageCapacityGB,
                            Price = v.CurrentPrice,
                            StockQuantity = v.StockQuantity
                        }).ToList(),

                    // Images
                    Images = l.Images.Select(i => new LaptopImageDTO
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        IsMain = i.IsMain
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            if (!laptopsDto.Any())
            {
                return ServiceResponse<PagedResult<LaptopsDto>>.NotFoundResponse();
            }
            var pagedResult = new PagedResult<LaptopsDto>
            (
                laptopsDto,
                laptopsDto.Count,
                1,
                1
            );
            return ServiceResponse<PagedResult<LaptopsDto>>.SuccessResponse(pagedResult, "","");
        }
    }
}