using TechZone.Api.DTOs.Laptop;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Services.Interfaces
{
    public interface ILaptopService
    {
        Task<ServiceResponse<IEnumerable<Laptop>>> GetAllAsync();
        Task<ServiceResponse<Laptop>> GetByIdAsync(int id);
        Task<ServiceResponse<Laptop>> CreateAsync(CreateLaptopDto laptop);
        Task<ServiceResponse<Laptop>> UpdateAsync(int id, UpdateLaptopDto laptop);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
        Task<ServiceResponse<IEnumerable<Laptop>>> SearchAsync(string searchTerm);
        Task<ServiceResponse<IEnumerable<Laptop>>> GetBySpecificationsAsync(string? processor = null, string? gpu = null, int? minPrice = null, int? maxPrice = null);
    }
}
