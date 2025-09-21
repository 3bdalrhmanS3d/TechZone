using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechZone.Core.Entities.User;
using TechZone.Core.ServiceResponse;

namespace TechZone.Core.Service.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generate JWT token for user
        /// </summary>
        /// <param name="user">Application user</param>
        /// <returns>JWT Security Token</returns>
        Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user);

        /// <summary>
        /// Generate refresh token
        /// </summary>
        /// <returns>Refresh token</returns>
        RefreshToken GenerateRefreshToken();

        /// <summary>
        /// Validate JWT token
        /// </summary>
        /// <param name="token">JWT token string</param>
        /// <returns>Service response with validation result</returns>
        ServiceResponse<ClaimsPrincipal> ValidateToken(string token);

        /// <summary>
        /// Extract user ID from JWT token
        /// </summary>
        /// <param name="token">JWT token string</param>
        /// <returns>Service response with user ID</returns>
        ServiceResponse<string> GetUserIdFromToken(string token);

        /// <summary>
        /// Check if token is expired
        /// </summary>
        /// <param name="token">JWT token string</param>
        /// <returns>True if expired, false otherwise</returns>
        bool IsTokenExpired(string token);

        /// <summary>
        /// Get token expiration time
        /// </summary>
        /// <param name="token">JWT token string</param>
        /// <returns>Service response with expiration time</returns>
        ServiceResponse<DateTime> GetTokenExpiration(string token);

        /// <summary>
        /// Revoke refresh token
        /// </summary>
        /// <param name="user">Application user</param>
        /// <param name="refreshToken">Refresh token to revoke</param>
        /// <returns>Service response with operation result</returns>
        Task<ServiceResponse<bool>> RevokeRefreshTokenAsync(ApplicationUser user, string refreshToken);

        /// <summary>
        /// Clean expired refresh tokens for user
        /// </summary>
        /// <param name="user">Application user</param>
        /// <returns>Service response with cleanup result</returns>
        Task<ServiceResponse<int>> CleanExpiredRefreshTokensAsync(ApplicationUser user);
    }
}