using MediatR;
using Microsoft.AspNetCore.Identity;
using TechZone.Domain.Entities.User;
using TechZone.Domain.ServiceResponse;
using TechZoneV1.Features.Profile.EditUserProfile.Commands;

namespace TechZoneV1.Features.Profile.EditUserProfile.Handlers
{
    public record EditProfileCommandHandler : IRequestHandler<EditProfileCommand, ServiceResponse<bool>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EditProfileCommandHandler(UserManager<ApplicationUser> userManager ,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<bool>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return ServiceResponse<bool>.UnauthorizedResponse();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResponse<bool>.NotFoundResponse("User not found", "المستخدم غير موجود");
            }
            // Update user properties

            user.FullName = request.ProfileDto.FullName;
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return ServiceResponse<bool>.SuccessResponse(true, "Profile updated successfully", "تم تحديث الملف الشخصي بنجاح");
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ServiceResponse<bool>.ErrorResponse("Failed to update profile", "فشل في تحديث الملف الشخصي", 400);
            }
        }
    }

}
