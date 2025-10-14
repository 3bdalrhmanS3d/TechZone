using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.Entities.User;
using TechZone.Domain.ServiceResponse;
using TechZone.Features.Profile.Dtos;

namespace TechZone.Features.Profile.Queries
{
    public record GetProfileQuery : IRequest<ServiceResponse<ProfileDto>>;

    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ServiceResponse<ProfileDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<GetProfileQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetProfileQueryHandler(
            UserManager<ApplicationUser> userManager,
            ILogger<GetProfileQueryHandler> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<ProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Get the user id from the token using ClaimTypes.NameIdentifier
                var userId = GetUserIdFromToken();

                if (string.IsNullOrEmpty(userId))
                {
                    return ServiceResponse<ProfileDto>.UnauthorizedResponse("Invalid token", "رمز الدخول غير صالح");
                }

                var user = await _userManager.FindByNameAsync(userId);

                if (user == null)
                {
                    return ServiceResponse<ProfileDto>.NotFoundResponse("User not found", "المستخدم غير موجود");
                }

                var profileDto = new ProfileDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    LastUpdated = user.UpdatedAt,
                    Image = user.ProfileImageUrl
                };

                _logger.LogInformation("Profile retrieved for user {UserId}", userId);
                return ServiceResponse<ProfileDto>.SuccessResponse(profileDto, "Profile retrieved successfully", "تم استرجاع الملف الشخصي بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving profile");
                return ServiceResponse<ProfileDto>.InternalServerErrorResponse("An error occurred while retrieving profile", "حدث خطأ أثناء استرجاع الملف الشخصي");
            }
        }

        private string GetUserIdFromToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                // Based on your login, the user ID is stored in ClaimTypes.NameIdentifier
                return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return null;
        }
    }
}