using TechZone.Core.models;

namespace TechZone.Api.Services.Interfaces
{
    public interface ILaptopService
    {
        Task<IEnumerable<Laptop>> GetAllAsync();
        Task<Laptop?> GetByIdAsync(int id);
        Task<Laptop> CreateAsync(Laptop laptop);
        Task<Laptop?> UpdateAsync(int id, Laptop laptop);
        Task<bool> DeleteAsync(int id);
    }
}
