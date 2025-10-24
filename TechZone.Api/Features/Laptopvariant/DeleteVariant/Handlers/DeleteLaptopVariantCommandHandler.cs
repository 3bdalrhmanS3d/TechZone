using MediatR;
using TechZone.Domain.Entities;
using TechZone.Domain.Interfaces;
using TechZoneV1.Features.LaptopVariant.DeleteVariant.Commands;
using TechZoneV1.Features.LaptopVariant.DeleteVariant.ViewModels;
using TechZoneV1.Features.Shared;

namespace TechZoneV1.Features.LaptopVariant.DeleteVariant.Handlers
{
    public class DeleteLaptopVariantCommandHandler : IRequestHandler<DeleteLaptopVariantCommand, RequestResponse<DeleteLaptopVariantViewModel>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.LaptopVariant> _laptopVariantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLaptopVariantCommandHandler(
            IBaseRepository<TechZone.Domain.Entities.LaptopVariant> laptopVariantRepository,
            IUnitOfWork unitOfWork)
        {
            _laptopVariantRepository = laptopVariantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestResponse<DeleteLaptopVariantViewModel>> Handle(
            DeleteLaptopVariantCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Get existing laptop variant
                var existingVariant = await _laptopVariantRepository.GetByIdAsync(request.Id);
                if (existingVariant == null)
                {
                    return RequestResponse<DeleteLaptopVariantViewModel>.Fail(
                        "Laptop variant not found",
                        "نوع اللابتوب غير موجود"
                    );
                }

                // Check if variant has reserved stock
                if (existingVariant.ReservedQuantity > 0)
                {
                    return RequestResponse<DeleteLaptopVariantViewModel>.Fail(
                        "Cannot delete variant with reserved stock. Please fulfill or cancel pending orders first.",
                        "لا يمكن حذف النوع الذي يحتوي على مخزون محجوز. يرجى إكمال الطلبات المعلقة أو إلغاؤها أولاً."
                    );
                }

                // Store ID for response before deletion
                var variantId = existingVariant.Id;
                var deletedAt = DateTime.UtcNow;

                // Perform hard delete
                _laptopVariantRepository.Delete(existingVariant);
                await _unitOfWork.SaveChangesAsync();

                // Map to ViewModel
                var viewModel = new DeleteLaptopVariantViewModel
                {
                    Id = variantId,
                    DeletedAt = deletedAt
                };

                return RequestResponse<DeleteLaptopVariantViewModel>.Success(
                    viewModel,
                    "Laptop variant deleted successfully",
                    "تم حذف نوع اللابتوب بنجاح"
                );
            }
            catch (Exception ex)
            {
                // Log exception here
                return RequestResponse<DeleteLaptopVariantViewModel>.Fail(
                    "An error occurred while deleting laptop variant",
                    "حدث خطأ أثناء حذف نوع اللابتوب"
                );
            }
        }
    }
}