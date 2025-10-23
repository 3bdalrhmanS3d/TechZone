using Mapster;
using TechZoneV1.Features.Laptops.GetAllLaptops.Dtos;
using TechZoneV1.Features.Laptops.GetAllLaptops.ViewModels;

namespace TechZoneV1.Features.Shared.mapper
{
    public class LaptopMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LaptopsDto, LaptopResponseViewModel>();
            config.NewConfig<BrandDTO, LaptopResponseViewModel.BrandVM>();
            config.NewConfig<CategoryDTO, LaptopResponseViewModel.CategoryVM>();
            config.NewConfig<LaptopVariantDTO, LaptopResponseViewModel.LaptopVariantVM>();
            config.NewConfig<LaptopImageDTO, LaptopResponseViewModel.LaptopImageVM>();
        }
    }
}
