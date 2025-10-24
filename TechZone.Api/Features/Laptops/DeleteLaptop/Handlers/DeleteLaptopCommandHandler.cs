using MediatR;
using Microsoft.EntityFrameworkCore;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.Laptops.DeleteLaptop.Commands;
using TechZoneV1.Features.Laptops.DeleteLaptop.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.Laptops.DeleteLaptop.Handlers
{
    public class DeleteLaptopCommandHandler : IRequestHandler<DeleteLaptopCommand, RequestResponse<LaptopDeletedViewModel>>
    {
        private readonly IBaseRepository<Laptop> _laptopRepository;
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLaptopCommandHandler(
            IBaseRepository<Laptop> laptopRepository,
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopRepository = laptopRepository;
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<LaptopDeletedViewModel>> Handle(
            DeleteLaptopCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get existing laptop
                var laptop = await _laptopRepository.GetAll()
                    .Where(l => l.Id == request.Id && !l.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (laptop == null)
                {
                    return RequestResponse<LaptopDeletedViewModel>.Fail(
                        "Laptop not found",
                        "اللابتوب غير موجود"
                    );
                }

                // Check if laptop has active variants
                var hasActiveVariants = await _laptopVariantRepository.GetAll()
                    .AnyAsync(lv => lv.LaptopId == request.Id && lv.IsActive && !lv.IsDeleted, cancellationToken);

                if (hasActiveVariants)
                {
                    return RequestResponse<LaptopDeletedViewModel>.Fail(
                        "Cannot delete laptop with active variants. Please deactivate or delete variants first.",
                        "لا يمكن حذف اللابتوب مع وجود أنواع نشطة. يرجى إلغاء تفعيل أو حذف الأنواع أولاً."
                    );
                }

                // Check if laptop has any associated orders (optional - if you want to prevent deletion of laptops with order history)
                // var hasOrders = await CheckIfLaptopHasOrders(request.Id, cancellationToken);
                // if (hasOrders)
                // {
                //     return RequestResponse<LaptopDeletedViewModel>.Fail(
                //         "Cannot delete laptop with order history",
                //         "لا يمكن حذف اللابتوب مع وجود سجل طلبات"
                //     );
                // }

                // Perform soft delete using repository method
                _laptopRepository.Delete(laptop);
                await _unitOfWork.SaveChangesAsync();

                // Map to ViewModel
                var response = new LaptopDeletedViewModel
                {
                    Id = laptop.Id,
                    DeletedAt = laptop.DeletedAt ?? DateTime.UtcNow
                };

                return RequestResponse<LaptopDeletedViewModel>.Success(
                    response,
                    "Laptop deleted successfully",
                    "تم حذف اللابتوب بنجاح"
                );
            }
            catch (DbUpdateException ex)
            {
                // Log database exception
                return RequestResponse<LaptopDeletedViewModel>.Fail(
                    "An error occurred while deleting laptop",
                    "حدث خطأ أثناء حذف اللابتوب"
                );
            }
            catch (Exception ex)
            {
                // Log general exception
                return RequestResponse<LaptopDeletedViewModel>.Fail(
                    "An error occurred while deleting laptop",
                    "حدث خطأ أثناء حذف اللابتوب"
                );
            }
        }

        // Optional: Method to check if laptop has associated orders
        private async Task<bool> CheckIfLaptopHasOrders(int laptopId, CancellationToken cancellationToken)
        {
            // This would require a repository for OrderItems and a way to link them to laptops
            // Since OrderItem has ProductType and ProductId, we would need to check all variants of this laptop
            // For now, we'll skip this check since it's complex and might not be needed
            return false;
        }
    }
}