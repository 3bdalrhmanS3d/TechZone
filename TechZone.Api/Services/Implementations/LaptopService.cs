using TechZone.Api.Services.Interfaces;
using TechZone.Core.Interfaces;
using TechZone.Core.models;

namespace TechZone.Api.Services.Implementations
{
    public class LaptopService : ILaptopService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LaptopService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Laptop>> GetAllAsync()
        {
            return await _unitOfWork.Laptops.GetAllAsync();
        }

        public async Task<Laptop?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Laptops.GetByIdAsync(id);
        }

        public async Task<Laptop> CreateAsync(Laptop laptop)
        {
            await _unitOfWork.Laptops.AddAsync(laptop);
            await _unitOfWork.CompleteAsync();
            return laptop;
        }

        public async Task<Laptop?> UpdateAsync(int id, Laptop laptop)
        {
            var existingLaptop = await _unitOfWork.Laptops.GetByIdAsync(id);
            if (existingLaptop == null) return null;
            
            existingLaptop.ModelName = laptop.ModelName;
            existingLaptop.Processor = laptop.Processor;
            existingLaptop.GPU = laptop.GPU;
            existingLaptop.ScreenSize = laptop.ScreenSize;
            existingLaptop.HasCamera = laptop.HasCamera;
            existingLaptop.HasKeyboard = laptop.HasKeyboard;
            existingLaptop.HasTouchScreen = laptop.HasTouchScreen;
            existingLaptop.Ports = laptop.Ports;


            _unitOfWork.Laptops.Update(existingLaptop);
            await _unitOfWork.CompleteAsync();

            return existingLaptop;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var laptop = await _unitOfWork.Laptops.GetByIdAsync(id);
            if (laptop == null) return false;

            _unitOfWork.Laptops.Delete(laptop);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}