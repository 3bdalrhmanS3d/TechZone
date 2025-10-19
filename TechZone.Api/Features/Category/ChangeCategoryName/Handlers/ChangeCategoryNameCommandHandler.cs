using MediatR;
using TechZone.Domain.Interfaces;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Category.ChangeCategoryName.Commands;
using TechZoneV1.Features.Category.ChangeCategoryName.Dtos;
using TechZone.Domain.Entities;
namespace TechZoneV1.Features.Category.ChangeCategoryName.Handlers
{
    public record ChangeCategoryNameCommandHandler : IRequestHandler<ChangeCategoryNameCommand, ServiceResponse<ChangeCategoryNameResponseDto>>
    {
        private readonly IBaseRepository<TechZone.Domain.Entities.Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeCategoryNameCommandHandler(IBaseRepository<TechZone.Domain.Entities.Category> categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            
        }
        public async Task<ServiceResponse<ChangeCategoryNameResponseDto>> Handle(ChangeCategoryNameCommand request, CancellationToken cancellationToken)
        {
            //var categorytst = await _categoryRepository.GetByIdAsync(request.RequestDto.Id);
            //if (category == null)
            //{
            //    return ServiceResponse<ChangeCategoryNameResponseDto>.NotFoundResponse();
            //}

            //var oldname = category.Name;
            //category.Name = request.RequestDto.NewName;
            //category.UpdatedAt = DateTime.UtcNow;
            var editedcategory = new TechZone.Domain.Entities.Category
            {
                Id = request.RequestDto.Id,
                Name = request.RequestDto.NewName,
                UpdatedAt = DateTime.UtcNow
            };
            var category = await _categoryRepository.GetByIdAsync(editedcategory.Id);
            string oldname = category != null ? category.Name : string.Empty;

            _categoryRepository.SaveInclude(editedcategory, nameof(editedcategory.Name),nameof(editedcategory.UpdatedAt));


            int res =  await _unitOfWork.SaveChangesAsync();

            var responseDto = new ChangeCategoryNameResponseDto { Id = editedcategory.Id, Name = editedcategory.Name, UpdatedAt = editedcategory.UpdatedAt , oldname= oldname};


            if(res <= 0)
            {
                return ServiceResponse<ChangeCategoryNameResponseDto>.ErrorResponse("Failed to change category name", "فشل في تغيير اسم الفئة", 500);
            }
           
            return ServiceResponse<ChangeCategoryNameResponseDto>.SuccessResponse(
                responseDto,
                "Category name changed successfully",
                "تم تغيير اسم الفئة بنجاح"
            );
        }
    }

}
