using TechZone.Api.DTOs.Auth;

namespace TechZone.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterDto model);
        Task<AuthDto> GetTokenAsync(TokenRequestDto model);
        Task<string> AddRoleAsync(AddRoleDto model);

    }
}
