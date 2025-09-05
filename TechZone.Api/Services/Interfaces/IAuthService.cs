using TechZone.Api.DTOs.Auth;

namespace TechZone.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterDto model);

    }
}
