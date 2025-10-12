using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechZone.DTOs.Auth;
using TechZone.Domain.Service.Interfaces;
using TechZone.Domain.ServiceResponse;
using TechZone.Shared.Extensions;

namespace TechZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="dto">Registration details</param>
        /// <returns>Registration result - email verification code will be sent</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 409)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> Register([FromBody] RegisterDto dto)
        {
            _logger.LogInformation("POST /api/auth/register - Registration attempt for email: {Email}", dto.Email);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<bool>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.RegisterAsync(dto);
            return response.ToActionResult();
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="dto">Login credentials</param>
        /// <returns>Authentication result with JWT token</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 401)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 403)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 500)]
        public async Task<ActionResult<ServiceResponse<AuthDto>>> Login([FromBody] TokenRequestDto dto)
        {
            _logger.LogInformation("POST /api/auth/login - Login attempt for email: {Email}", dto.Email);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<AuthDto>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.GetTokenAsync(dto);

            // Set refresh token in cookie if successful
            if (response.IsSuccess && !string.IsNullOrEmpty(response.Data?.RefreshToken))
            {
                SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
            }

            return response.ToActionResult();
        }

        /// <summary>
        /// Confirm user email with verification code
        /// </summary>
        /// <param name="dto">Email confirmation details with code</param>
        /// <returns>Confirmation result</returns>
        [HttpPost("confirm-email")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> ConfirmEmail([FromBody] ConfirmEmailWithCodeDto dto)
        {
            _logger.LogInformation("POST /api/auth/confirm-email - Email confirmation for user: {Email}", dto.Email);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<bool>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.ConfirmEmailAsync(dto);
            return response.ToActionResult();
        }

        /// <summary>
        /// Resend verification code
        /// </summary>
        /// <param name="dto">Resend verification code details</param>
        /// <returns>Resend result</returns>
        [HttpPost("resend-verification-code")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> ResendVerificationCode([FromBody] ResendVerificationCodeDto dto)
        {
            _logger.LogInformation("POST /api/auth/resend-verification-code - Resending {Type} code to: {Email}",
                dto.VerificationType, dto.Email);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<bool>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.ResendVerificationCodeAsync(dto);
            return response.ToActionResult();
        }

        /// <summary>
        /// Send forgot password email with reset code
        /// </summary>
        /// <param name="dto">Forgot password details</param>
        /// <returns>Send result</returns>
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            _logger.LogInformation("POST /api/auth/forgot-password - Password reset request for: {Email}", dto.Email);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<bool>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.ForgotPasswordAsync(dto);
            return response.ToActionResult();
        }

        /// <summary>
        /// Reset user password with verification code
        /// </summary>
        /// <param name="dto">Password reset details with code</param>
        /// <returns>Reset result</returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> ResetPassword([FromBody] ResetPasswordWithCodeDto dto)
        {
            _logger.LogInformation("POST /api/auth/reset-password - Password reset for: {Email}", dto.Email);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<bool>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.ResetPasswordAsync(dto);
            return response.ToActionResult();
        }

        /// <summary>
        /// Change user password (requires authentication)
        /// </summary>
        /// <param name="dto">Password change details</param>
        /// <returns>Change result</returns>
        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 401)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                var errorResponse = ServiceResponse<bool>.UnauthorizedResponse("User not found in token");
                return errorResponse.ToActionResult();
            }

            _logger.LogInformation("POST /api/auth/change-password - Password change for user: {UserId}", userId);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<bool>();
                return validationResponse.ToActionResult();
            }

            var response = await _authService.ChangePasswordAsync(userId, dto);
            return response.ToActionResult();
        }

        /// <summary>
        /// Refresh JWT token using refresh token
        /// </summary>
        /// <param name="dto">Refresh token details</param>
        /// <returns>New authentication tokens</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 401)]
        [ProducesResponseType(typeof(ServiceResponse<AuthDto>), 500)]
        public async Task<ActionResult<ServiceResponse<AuthDto>>> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            _logger.LogInformation("POST /api/auth/refresh-token - Token refresh request");

            // If no token in body, try to get from cookie
            if (string.IsNullOrEmpty(dto.RefreshToken))
            {
                dto.RefreshToken = Request.Cookies["refreshToken"] ?? string.Empty;
            }

            if (string.IsNullOrEmpty(dto.RefreshToken))
            {
                var errorResponse = ServiceResponse<AuthDto>.ErrorResponse("Refresh token is required",
                    "رمز التحديث مطلوب",
                    400);
                return errorResponse.ToActionResult();
            }

            var response = await _authService.RefreshTokenAsync(dto);

            // Set new refresh token in cookie if successful
            if (response.IsSuccess && !string.IsNullOrEmpty(response.Data?.RefreshToken))
            {
                SetRefreshTokenInCookie(response.Data.RefreshToken, response.Data.RefreshTokenExpiration);
            }

            return response.ToActionResult();
        }

        /// <summary>
        /// Revoke refresh token
        /// </summary>
        /// <param name="dto">Token to revoke</param>
        /// <returns>Revoke result</returns>
        [HttpPost("revoke-token")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 401)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> RevokeToken([FromBody] RevokeTokenDto dto)
        {
            var userId = User.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                var errorResponse = ServiceResponse<bool>.UnauthorizedResponse("User not found in token");
                return errorResponse.ToActionResult();
            }

            _logger.LogInformation("POST /api/auth/revoke-token - Token revoke for user: {UserId}", userId);

            // If no token in body, try to get from cookie
            if (string.IsNullOrEmpty(dto.RefreshToken))
            {
                dto.RefreshToken = Request.Cookies["refreshToken"] ?? string.Empty;
            }

            if (string.IsNullOrEmpty(dto.RefreshToken))
            {
                var errorResponse = ServiceResponse<bool>.ErrorResponse("Refresh token is required",
                    "رمز التحديث مطلوب", 400);
                return errorResponse.ToActionResult();
            }

            var response = await _authService.RevokeTokenAsync(userId, dto);

            // Clear cookie if successful
            if (response.IsSuccess)
            {
                Response.Cookies.Delete("refreshToken");
            }

            return response.ToActionResult();
        }

        /// <summary>
        /// Logout user and clear refresh token
        /// </summary>
        /// <returns>Logout result</returns>
        [HttpPost("logout")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> Logout()
        {
            _logger.LogInformation("POST /api/auth/logout - User logout");

            try
            {
                // If user is authenticated, optionally revoke their refresh token
                var userId = User.FindFirst("uid")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var refreshToken = Request.Cookies["refreshToken"];
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        var revokeDto = new RevokeTokenDto { RefreshToken = refreshToken };
                        await _authService.RevokeTokenAsync(userId, revokeDto);
                    }
                }

                // Clear refresh token cookie
                Response.Cookies.Delete("refreshToken");

                var response = ServiceResponse<bool>.SuccessResponse(true, "Logout successful");
                return response.ToActionResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                var errorResponse = ServiceResponse<bool>.InternalServerErrorResponse("An error occurred during logout");
                return errorResponse.ToActionResult();
            }
        }

        /// <summary>
        /// Check authentication status
        /// </summary>
        /// <returns>Authentication status information</returns>
        [HttpGet("status")]
        [ProducesResponseType(typeof(ServiceResponse<object>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<object>), 500)]
        public ActionResult<ServiceResponse<object>> GetAuthStatus()
        {
            try
            {
                var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

                var status = new
                {
                    IsAuthenticated = isAuthenticated,
                    Username = User.Identity?.Name,
                    UserId = User.FindFirst("uid")?.Value,
                    Email = User.FindFirst(ClaimTypes.Email)?.Value,
                    Roles = User.FindAll("roles").Select(c => c.Value).ToList(),
                    TokenExpiry = User.FindFirst(ClaimTypes.Expiration)?.Value
                };

                var response = ServiceResponse<object>.SuccessResponse(status, "Authentication status retrieved successfully");
                return response.ToActionResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving authentication status");
                var errorResponse = ServiceResponse<object>.InternalServerErrorResponse("An error occurred while retrieving status");
                return errorResponse.ToActionResult();
            }
        }

        #region Helper Methods

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        #endregion
    }
}