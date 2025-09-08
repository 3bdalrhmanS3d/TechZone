using TechZone.Api.DTOs.Auth;
using TechZone.Core.ServiceResponse;

namespace TechZone.Core.Service.Interfaces
{
    public interface IAuthService
    {
        // User Registration and Email Confirmation
        Task<ServiceResponse<bool>> RegisterAsync(RegisterDto dto);
        Task<ServiceResponse<bool>> ConfirmEmailAsync(ConfirmEmailWithCodeDto dto);
        Task<ServiceResponse<bool>> ResendVerificationCodeAsync(ResendVerificationCodeDto dto);

        // Authentication
        Task<ServiceResponse<AuthDto>> GetTokenAsync(TokenRequestDto dto);
        Task<ServiceResponse<AuthDto>> RefreshTokenAsync(RefreshTokenDto dto);
        Task<ServiceResponse<bool>> RevokeTokenAsync(string userId, RevokeTokenDto dto);

        // Password Management
        Task<ServiceResponse<bool>> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<ServiceResponse<bool>> ResetPasswordAsync(ResetPasswordWithCodeDto dto);
        Task<ServiceResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto dto);

        // Role Management
        Task<ServiceResponse<bool>> AddRoleAsync(AddRoleDto dto);
    }
}