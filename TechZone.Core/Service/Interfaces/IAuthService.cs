using TechZone.Api.DTOs.Auth;
using TechZone.Core.ServiceResponse;

public interface IAuthService
{
    
    Task<ServiceResponse<AuthDto>> RegisterAsync(RegisterDto model);

    Task<ServiceResponse<AuthDto>> GetTokenAsync(TokenRequestDto model);

    Task<ServiceResponse<bool>> ConfirmEmailAsync(ConfirmEmailDto model);

    Task<ServiceResponse<bool>> ResendEmailConfirmationAsync(ResendConfirmationEmailDto model);

    Task<ServiceResponse<bool>> ForgotPasswordAsync(ForgotPasswordDto model);

    Task<ServiceResponse<bool>> ResetPasswordAsync(ResetPasswordDto model);

    Task<ServiceResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto model);

    Task<ServiceResponse<AuthDto>> RefreshTokenAsync(RefreshTokenDto model);

    Task<ServiceResponse<bool>> RevokeTokenAsync(string userId, RevokeTokenDto model);

    
}