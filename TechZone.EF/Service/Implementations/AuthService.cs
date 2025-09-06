using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TechZone.Api.DTOs.Auth;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.Entities;
using TechZone.Core.models;
using TechZone.Core.ServiceResponse;

namespace TechZone.EF.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtService jwtService,
            IEmailService emailService,
            ILogger<AuthService> logger,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _emailService = emailService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<AuthDto>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                // Check if email is already registered
                if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                {
                    return ServiceResponse<AuthDto>.ConflictResponse(
                        "Email is already registered");
                }

                // Check if username is already registered
                if (await _userManager.FindByNameAsync(dto.UserName) is not null)
                {
                    return ServiceResponse<AuthDto>.ConflictResponse(
                        "Username is already registered");
                }

                var user = new ApplicationUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    FullName = dto.FullName,
                    EmailConfirmed = false // Require email confirmation
                };

                var result = await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return ServiceResponse<AuthDto>.ValidationErrorResponse(
                        new Dictionary<string, List<string>> { { "Password", errors } },
                        "Registration failed");
                }

                // Add user to default role
                await _userManager.AddToRoleAsync(user, "User");

                // Generate email confirmation token
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = GenerateEmailConfirmationLink(user.Id, emailConfirmationToken);

                // Send confirmation email
                var emailResult = await _emailService.SendEmailConfirmationAsync(user.Email, confirmationLink);
                if (!emailResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to send confirmation email to {Email}", user.Email);
                    // Don't fail registration if email fails, just log it
                }

                // Generate JWT token (user can use the app but some features may be limited until email is confirmed)
                var jwtSecurityToken = await _jwtService.CreateJwtTokenAsync(user);
                var refreshToken = _jwtService.GenerateRefreshToken();

                // Add refresh token to user
                if (user.RefreshTokens == null)
                    user.RefreshTokens = new List<RefreshToken>();

                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);

                var authDto = new AuthDto
                {
                    Email = user.Email,
                    IsAuthenticated = true,
                    Roles = new List<string> { "User" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Username = user.UserName,
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpiresOn,
                    EmailConfirmed = user.EmailConfirmed
                };

                _logger.LogInformation("User {Email} registered successfully", user.Email);
                return ServiceResponse<AuthDto>.SuccessResponse(authDto, "Registration completed successfully. Please check your email to confirm your account.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration for email {Email}", dto.Email);
                return ServiceResponse<AuthDto>.InternalServerErrorResponse(
                    "An error occurred during registration");
            }
        }

        public async Task<ServiceResponse<AuthDto>> GetTokenAsync(TokenRequestDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                {
                    return ServiceResponse<AuthDto>.UnauthorizedResponse(
                        "Invalid email or password");
                }

                // Optional: Check if email is confirmed
                if (!user.EmailConfirmed)
                {
                    return ServiceResponse<AuthDto>.ErrorResponse(
                        "Email not confirmed. Please check your email and confirm your account.",
                        "البريد الإلكتروني غير مؤكد. يرجى التحقق من بريدك الإلكتروني وتأكيد حسابك.",
                        403);
                }

                var jwtSecurityToken = await _jwtService.CreateJwtTokenAsync(user);
                var rolesList = await _userManager.GetRolesAsync(user);

                // Handle refresh tokens
                RefreshToken refreshToken;
                if (user.RefreshTokens?.Any(t => t.IsActive) == true)
                {
                    refreshToken = user.RefreshTokens.First(t => t.IsActive);
                }
                else
                {
                    refreshToken = _jwtService.GenerateRefreshToken();
                    if (user.RefreshTokens == null)
                        user.RefreshTokens = new List<RefreshToken>();

                    user.RefreshTokens.Add(refreshToken);
                    await _userManager.UpdateAsync(user);
                }

                var authDto = new AuthDto
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    Username = user.UserName,
                    Roles = rolesList.ToList(),
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpiresOn,
                    EmailConfirmed = user.EmailConfirmed
                };

                _logger.LogInformation("User {Email} logged in successfully", user.Email);
                return ServiceResponse<AuthDto>.SuccessResponse(authDto, "Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email {Email}", dto.Email);
                return ServiceResponse<AuthDto>.InternalServerErrorResponse(
                    "An error occurred during login");
            }
        }

        public async Task<ServiceResponse<bool>> AddRoleAsync(AddRoleDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.UserId);
                if (user is null)
                {
                    return ServiceResponse<bool>.NotFoundResponse("User not found");
                }

                if (!await _roleManager.RoleExistsAsync(dto.Role))
                {
                    return ServiceResponse<bool>.NotFoundResponse("Role does not exist");
                }

                if (await _userManager.IsInRoleAsync(user, dto.Role))
                {
                    return ServiceResponse<bool>.ConflictResponse("User already assigned to this role");
                }

                var result = await _userManager.AddToRoleAsync(user, dto.Role);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return ServiceResponse<bool>.ErrorResponse(errors, "Failed to add role to user");
                }

                _logger.LogInformation("Role {Role} added to user {UserId}", dto.Role, dto.UserId);
                return ServiceResponse<bool>.SuccessResponse(true, "Role added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding role {Role} to user {UserId}", dto.Role, dto.UserId);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while adding role");
            }
        }

        public async Task<ServiceResponse<bool>> ConfirmEmailAsync(ConfirmEmailDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.UserId);
                if (user is null)
                {
                    return ServiceResponse<bool>.NotFoundResponse("User not found");
                }

                if (user.EmailConfirmed)
                {
                    return ServiceResponse<bool>.SuccessResponse(true, "Email is already confirmed");
                }

                var decodedToken = WebEncoders.Base64UrlDecode(dto.Token);
                var normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ConfirmEmailAsync(user, normalToken);
                if (!result.Succeeded)
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        "Invalid or expired confirmation token",
                        "رمز التأكيد غير صالح أو منتهي الصلاحية",
                        400);
                }

                // Send welcome email
                var welcomeEmailResult = await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);
                if (!welcomeEmailResult.IsSuccess)
                {
                    _logger.LogWarning("Failed to send welcome email to {Email}", user.Email);
                }

                _logger.LogInformation("Email confirmed for user {UserId}", dto.UserId);
                return ServiceResponse<bool>.SuccessResponse(true, "Email confirmed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming email for user {UserId}", dto.UserId);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while confirming email");
            }
        }

        public async Task<ServiceResponse<bool>> ResendEmailConfirmationAsync(ResendConfirmationEmailDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user is null)
                {
                    // Don't reveal if email exists or not for security
                    return ServiceResponse<bool>.SuccessResponse(true, "If the email exists, a confirmation link has been sent");
                }

                if (user.EmailConfirmed)
                {
                    return ServiceResponse<bool>.SuccessResponse(true, "Email is already confirmed");
                }

                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = GenerateEmailConfirmationLink(user.Id, emailConfirmationToken);

                var emailResult = await _emailService.SendEmailConfirmationAsync(user.Email, confirmationLink);
                if (!emailResult.IsSuccess)
                {
                    return ServiceResponse<bool>.InternalServerErrorResponse(
                        "Failed to send confirmation email");
                }

                _logger.LogInformation("Email confirmation resent to {Email}", dto.Email);
                return ServiceResponse<bool>.SuccessResponse(true, "Confirmation email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resending email confirmation to {Email}", dto.Email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while resending confirmation email");
            }
        }

        public async Task<ServiceResponse<bool>> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user is null)
                {
                    // Don't reveal if email exists or not for security
                    return ServiceResponse<bool>.SuccessResponse(true, "If the email exists, a password reset link has been sent");
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = GeneratePasswordResetLink(user.Email, resetToken);

                var emailResult = await _emailService.SendPasswordResetAsync(user.Email, resetLink);
                if (!emailResult.IsSuccess)
                {
                    return ServiceResponse<bool>.InternalServerErrorResponse(
                        "Failed to send password reset email");
                }

                _logger.LogInformation("Password reset email sent to {Email}", dto.Email);
                return ServiceResponse<bool>.SuccessResponse(true, "Password reset email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending password reset email to {Email}", dto.Email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while sending password reset email");
            }
        }

        public async Task<ServiceResponse<bool>> ResetPasswordAsync(ResetPasswordDto dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user is null)
                {
                    return ServiceResponse<bool>.NotFoundResponse("User not found");
                }

                var decodedToken = WebEncoders.Base64UrlDecode(dto.Token);
                var normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ResetPasswordAsync(user, normalToken, dto.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return ServiceResponse<bool>.ValidationErrorResponse(
                        new Dictionary<string, List<string>> { { "Password", errors } },
                        "Password reset failed");
                }

                // Revoke all refresh tokens for security
                if (user.RefreshTokens?.Any() == true)
                {
                    foreach (var token in user.RefreshTokens.Where(t => t.IsActive))
                    {
                        token.RevokedOn = DateTime.UtcNow;
                    }
                    await _userManager.UpdateAsync(user);
                }

                _logger.LogInformation("Password reset successfully for user {Email}", dto.Email);
                return ServiceResponse<bool>.SuccessResponse(true, "Password reset successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting password for email {Email}", dto.Email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while resetting password");
            }
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return ServiceResponse<bool>.NotFoundResponse("User not found");
                }

                var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return ServiceResponse<bool>.ValidationErrorResponse(
                        new Dictionary<string, List<string>> { { "Password", errors } },
                        "Password change failed");
                }

                _logger.LogInformation("Password changed successfully for user {UserId}", userId);
                return ServiceResponse<bool>.SuccessResponse(true, "Password changed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user {UserId}", userId);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while changing password");
            }
        }

        public async Task<ServiceResponse<AuthDto>> RefreshTokenAsync(RefreshTokenDto dto)
        {
            try
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == dto.RefreshToken));

                if (user is null)
                {
                    return ServiceResponse<AuthDto>.UnauthorizedResponse("Invalid refresh token");
                }

                var refreshToken = user.RefreshTokens.First(t => t.Token == dto.RefreshToken);
                if (!refreshToken.IsActive)
                {
                    return ServiceResponse<AuthDto>.UnauthorizedResponse("Inactive refresh token");
                }

                // Generate new tokens
                var jwtSecurityToken = await _jwtService.CreateJwtTokenAsync(user);
                var newRefreshToken = _jwtService.GenerateRefreshToken();

                // Revoke old refresh token
                refreshToken.RevokedOn = DateTime.UtcNow;
                user.RefreshTokens.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);

                var rolesList = await _userManager.GetRolesAsync(user);
                var authDto = new AuthDto
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    Username = user.UserName,
                    Roles = rolesList.ToList(),
                    RefreshToken = newRefreshToken.Token,
                    RefreshTokenExpiration = newRefreshToken.ExpiresOn,
                    EmailConfirmed = user.EmailConfirmed
                };

                return ServiceResponse<AuthDto>.SuccessResponse(authDto, "Token refreshed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token");
                return ServiceResponse<AuthDto>.InternalServerErrorResponse(
                    "An error occurred while refreshing token");
            }
        }

        public async Task<ServiceResponse<bool>> RevokeTokenAsync(string userId, RevokeTokenDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return ServiceResponse<bool>.NotFoundResponse("User not found");
                }

                return await _jwtService.RevokeRefreshTokenAsync(user, dto.RefreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error revoking token for user {UserId}", userId);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "An error occurred while revoking token");
            }
        }

        #region Private Helper Methods

        private string GenerateEmailConfirmationLink(string userId, string token)
        {
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var baseUrl = _configuration["AppSettings:ClientUrl"] ?? "https://localhost:7124";
            return $"{baseUrl}/api/auth/confirm-email?userId={userId}&token={encodedToken}";
        }

        private string GeneratePasswordResetLink(string email, string token)
        {
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var baseUrl = _configuration["AppSettings:ClientUrl"] ?? "https://localhost:7124";
            return $"{baseUrl}/api/auth/reset-password?email={email}&token={encodedToken}";
        }

        #endregion
    }
}