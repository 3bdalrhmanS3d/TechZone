using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.Entities;
using TechZone.Core.Service.Interfaces;
using TechZone.Core.ServiceResponse;

namespace TechZone.EF.Service.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Api.helper.JWT _jwtSettings;
        private readonly ILogger<JwtService> _logger;

        public JwtService(
            UserManager<ApplicationUser> userManager,
            IOptions<Api.helper.JWT> jwtSettings,
            ILogger<JwtService> logger)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        public async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
        {
            try
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = new List<Claim>();

                foreach (var role in roles)
                    roleClaims.Add(new Claim("roles", role));

                var fullName = $"{user.FirstName} {user.LastName}"; // Combine first and last name

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                    new Claim("uid", user.Id),
                    new Claim("fullName", fullName), // Updated to use combined name
                    new Claim(JwtRegisteredClaimNames.Iat,
                        new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64)
                }
                .Union(userClaims)
                .Union(roleClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                    signingCredentials: signingCredentials);

                _logger.LogInformation("JWT token created successfully for user: {UserId}", user.Id);

                return jwtSecurityToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating JWT token for user: {UserId}", user.Id);
                throw;
            }
        }

        // ... rest of the JwtService methods remain the same ...
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(7), // 7 days expiry
                CreatedOn = DateTime.UtcNow
            };
        }

        public ServiceResponse<ClaimsPrincipal> ValidateToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return ServiceResponse<ClaimsPrincipal>.ErrorResponse(
                        "Token is required", "الرمز مطلوب", 400);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                return ServiceResponse<ClaimsPrincipal>.SuccessResponse(
                    principal, "Token validated successfully");
            }
            catch (SecurityTokenExpiredException)
            {
                return ServiceResponse<ClaimsPrincipal>.ErrorResponse(
                    "Token has expired", "الرمز مطلوب", 401);
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning("Token validation failed: {Message}", ex.Message);
                return ServiceResponse<ClaimsPrincipal>.ErrorResponse(
                    "Invalid token", "الرمز مطلوب", 401);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating token");
                return ServiceResponse<ClaimsPrincipal>.InternalServerErrorResponse(
                    "Error occurred while validating token");
            }
        }

        public ServiceResponse<string> GetUserIdFromToken(string token)
        {
            try
            {
                var validationResult = ValidateToken(token);

                if (!validationResult.IsSuccess)
                {
                    return ServiceResponse<string>.ErrorResponse(
                        validationResult.Message,
                        validationResult.MessageAr,
                        validationResult.StatusCode);
                }

                var userId = validationResult.Data?.FindFirst("uid")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return ServiceResponse<string>.ErrorResponse(
                        "User ID not found in token",
                        "لم يتم العثور على معرف المستخدم في الرمز",
                        400);
                }

                return ServiceResponse<string>.SuccessResponse(
                    userId, "User ID extracted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting user ID from token");
                return ServiceResponse<string>.InternalServerErrorResponse(
                    "Error occurred while extracting user ID");
            }
        }

        public bool IsTokenExpired(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadJwtToken(token);

                return jsonToken.ValidTo <= DateTime.UtcNow;
            }
            catch
            {
                return true; // Consider invalid tokens as expired
            }
        }

        public ServiceResponse<DateTime> GetTokenExpiration(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return ServiceResponse<DateTime>.ErrorResponse(
                        "Token is required", "الرمز مطلوب", 400);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadJwtToken(token);

                return ServiceResponse<DateTime>.SuccessResponse(
                    jsonToken.ValidTo, "Token expiration retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting token expiration");
                return ServiceResponse<DateTime>.ErrorResponse(
                    "Invalid token format",
                    "الرمز مطلوب", 400);
            }
        }

        public async Task<ServiceResponse<bool>> RevokeRefreshTokenAsync(ApplicationUser user, string refreshToken)
        {
            try
            {
                var token = user.RefreshTokens?.FirstOrDefault(t => t.Token == refreshToken);

                if (token == null)
                {
                    return ServiceResponse<bool>.NotFoundResponse(
                        "Refresh token not found");
                }

                if (!token.IsActive)
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        "Refresh token is already inactive", "رمز التحديث غير نشط بالفعل", 400);
                }

                token.RevokedOn = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        "Failed to revoke refresh token",
                        "فشل في إبطال رمز التحديث",
                        500);
                }

                _logger.LogInformation("Refresh token revoked for user: {UserId}", user.Id);

                return ServiceResponse<bool>.SuccessResponse(
                    true, "Refresh token revoked successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking refresh token for user: {UserId}", user.Id);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while revoking refresh token");
            }
        }

        public async Task<ServiceResponse<int>> CleanExpiredRefreshTokensAsync(ApplicationUser user)
        {
            try
            {
                if (user.RefreshTokens == null || !user.RefreshTokens.Any())
                {
                    return ServiceResponse<int>.SuccessResponse(
                        0, "No refresh tokens to clean");
                }

                var expiredTokens = user.RefreshTokens.Where(t => !t.IsActive).ToList();
                var removedCount = expiredTokens.Count;

                foreach (var expiredToken in expiredTokens)
                {
                    user.RefreshTokens.Remove(expiredToken);
                }

                if (removedCount > 0)
                {
                    var result = await _userManager.UpdateAsync(user);

                    if (!result.Succeeded)
                    {
                        return ServiceResponse<int>.ErrorResponse(
                            "Failed to clean expired refresh tokens",
                            "فشل في تنظيف رموز التحديث منتهية الصلاحية",
                            500);
                    }

                    _logger.LogInformation("Cleaned {Count} expired refresh tokens for user: {UserId}",
                        removedCount, user.Id);
                }

                return ServiceResponse<int>.SuccessResponse(
                    removedCount, $"Cleaned {removedCount} expired refresh tokens");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning expired refresh tokens for user: {UserId}", user.Id);
                return ServiceResponse<int>.InternalServerErrorResponse(
                    "Error occurred while cleaning expired refresh tokens");
            }
        }
    }
}