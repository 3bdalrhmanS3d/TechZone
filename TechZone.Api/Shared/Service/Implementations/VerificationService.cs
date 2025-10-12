using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechZone.Services.Interfaces;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;
using TechZone.Domain.Service.Interfaces;
using TechZone.Domain.ServiceResponse;
using TechZone.Infrastructure.Application;

namespace TechZone.Shared.Service.Implementations
{
    public class VerificationService : IVerificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VerificationService> _logger;

        public VerificationService(
            ApplicationDbContext context,
            ILogger<VerificationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResponse<VerificationCode>> CreateVerificationCodeAsync(
            string userId,
            VerificationCodeType type,
            DestinationStatus destination,
            string? customCode = null)
        {
            try
            {
                // Invalidate any existing active codes of the same type for this user
                await InvalidateUserCodesAsync(userId, type);

                // Generate code if not provided
                var code = customCode ?? GenerateCodeByType(type);

                // Set expiry based on verification type
                var expiryMinutes = GetExpiryMinutesByType(type);
                var maxAttempts = GetMaxAttemptsByType(type);

                var verificationCode = new VerificationCode
                {
                    UserId = userId,
                    Code = code,
                    Type = type,
                    Destination = destination,
                    CreatedAt = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(expiryMinutes),
                    IsUsed = false,
                    AttemptCount = 0,
                    MaxAttempts = maxAttempts
                };

                await _context.VerificationCodes.AddAsync(verificationCode);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Verification code created for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<VerificationCode>.SuccessResponse(verificationCode, "Verification code created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating verification code for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<VerificationCode>.InternalServerErrorResponse("Error creating verification code");
            }
        }

        public async Task<ServiceResponse<bool>> VerifyCodeAsync(string userId, string code, VerificationCodeType type)
        {
            try
            {
                var verificationCode = await _context.VerificationCodes
                    .FirstOrDefaultAsync(vc =>
                        vc.UserId == userId &&
                        vc.Code == code &&
                        vc.Type == type &&
                        !vc.IsUsed);

                if (verificationCode == null)
                {
                    _logger.LogWarning("Invalid verification code attempt for user {UserId}, type {Type}", userId, type);
                    return ServiceResponse<bool>.ErrorResponse(
                        "Invalid verification code",
                        "رمز التحقق غير صالح",
                        400);
                }

                // Check if code is expired
                if (verificationCode.ExpiryDate < DateTime.UtcNow)
                {
                    _logger.LogWarning("Expired verification code used for user {UserId}, type {Type}", userId, type);
                    return ServiceResponse<bool>.ErrorResponse(
                        "Verification code has expired",
                        "رمز التحقق منتهي الصلاحية",
                        400);
                }

                // Check attempt count
                verificationCode.AttemptCount++;
                if (verificationCode.AttemptCount > verificationCode.MaxAttempts)
                {
                    verificationCode.IsUsed = true; // Block further attempts
                    await _context.SaveChangesAsync();

                    _logger.LogWarning("Too many verification attempts for user {UserId}, type {Type}", userId, type);
                    return ServiceResponse<bool>.ErrorResponse(
                        "Too many verification attempts. Please request a new code.",
                        "محاولات تحقق كثيرة جداً. يرجى طلب رمز جديد",
                        429);
                }

                // Mark as used on successful verification
                verificationCode.IsUsed = true;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Verification code verified successfully for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<bool>.SuccessResponse(true, "Verification code verified successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying code for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<bool>.InternalServerErrorResponse("Error verifying code");
            }
        }

        public async Task<ServiceResponse<bool>> IsCodeValidAsync(string userId, string code, VerificationCodeType type)
        {
            try
            {
                var isValid = await _context.VerificationCodes
                    .AnyAsync(vc =>
                        vc.UserId == userId &&
                        vc.Code == code &&
                        vc.Type == type &&
                        !vc.IsUsed &&
                        vc.ExpiryDate > DateTime.UtcNow &&
                        vc.AttemptCount < vc.MaxAttempts);

                return ServiceResponse<bool>.SuccessResponse(isValid,
                    isValid ? "Code is valid" : "Code is invalid or expired");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking code validity for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<bool>.InternalServerErrorResponse("Error checking code validity");
            }
        }

        public async Task<ServiceResponse<bool>> InvalidateUserCodesAsync(string userId, VerificationCodeType type)
        {
            try
            {
                var activeCodes = await _context.VerificationCodes
                    .Where(vc => vc.UserId == userId && vc.Type == type && !vc.IsUsed)
                    .ToListAsync();

                foreach (var code in activeCodes)
                {
                    code.IsUsed = true;
                }

                if (activeCodes.Any())
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Invalidated {Count} active codes for user {UserId}, type {Type}",
                        activeCodes.Count, userId, type);
                }

                return ServiceResponse<bool>.SuccessResponse(true, "User codes invalidated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invalidating codes for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<bool>.InternalServerErrorResponse("Error invalidating codes");
            }
        }

        public async Task<ServiceResponse<VerificationCode?>> GetActiveCodeAsync(string userId, VerificationCodeType type)
        {
            try
            {
                var activeCode = await _context.VerificationCodes
                    .FirstOrDefaultAsync(vc =>
                        vc.UserId == userId &&
                        vc.Type == type &&
                        !vc.IsUsed &&
                        vc.ExpiryDate > DateTime.UtcNow);

                return ServiceResponse<VerificationCode?>.SuccessResponse(activeCode,
                    activeCode != null ? "Active code found" : "No active code found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active code for user {UserId}, type {Type}", userId, type);
                return ServiceResponse<VerificationCode?>.InternalServerErrorResponse("Error getting active code");
            }
        }

        public async Task<ServiceResponse<bool>> CleanupExpiredCodesAsync()
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-7); // Keep expired codes for 7 days for audit

                var expiredCodes = await _context.VerificationCodes
                    .Where(vc => vc.ExpiryDate < cutoffDate)
                    .Take(1000) // Process in batches
                    .ToListAsync();

                if (expiredCodes.Any())
                {
                    _context.VerificationCodes.RemoveRange(expiredCodes);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Cleaned up {Count} expired verification codes", expiredCodes.Count);
                }

                return ServiceResponse<bool>.SuccessResponse(true, $"Cleaned up {expiredCodes.Count} expired codes");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cleaning up expired verification codes");
                return ServiceResponse<bool>.InternalServerErrorResponse("Error cleaning up expired codes");
            }
        }

        public async Task<ServiceResponse<Dictionary<string, object>>> GetVerificationStatsAsync()
        {
            try
            {
                var stats = new Dictionary<string, object>();

                // Total codes
                var totalCodes = await _context.VerificationCodes.CountAsync();
                stats["TotalCodes"] = totalCodes;

                // Active codes (not used and not expired)
                var activeCodes = await _context.VerificationCodes
                    .CountAsync(vc => !vc.IsUsed && vc.ExpiryDate > DateTime.UtcNow);
                stats["ActiveCodes"] = activeCodes;

                // Expired codes
                var expiredCodes = await _context.VerificationCodes
                    .CountAsync(vc => !vc.IsUsed && vc.ExpiryDate <= DateTime.UtcNow);
                stats["ExpiredCodes"] = expiredCodes;

                // Used codes
                var usedCodes = await _context.VerificationCodes
                    .CountAsync(vc => vc.IsUsed);
                stats["UsedCodes"] = usedCodes;

                // Codes by type
                var codesByType = await _context.VerificationCodes
                    .GroupBy(vc => vc.Type)
                    .Select(g => new { Type = g.Key.ToString(), Count = g.Count() })
                    .ToListAsync();
                stats["CodesByType"] = codesByType.ToDictionary(x => x.Type, x => x.Count);

                // Codes by destination
                var codesByDestination = await _context.VerificationCodes
                    .GroupBy(vc => vc.Destination)
                    .Select(g => new { Destination = g.Key.ToString(), Count = g.Count() })
                    .ToListAsync();
                stats["CodesByDestination"] = codesByDestination.ToDictionary(x => x.Destination, x => x.Count);

                // Recent codes (last 24 hours)
                var yesterday = DateTime.UtcNow.AddDays(-1);
                var recentCodes = await _context.VerificationCodes
                    .CountAsync(vc => vc.CreatedAt >= yesterday);
                stats["RecentCodes"] = recentCodes;

                return ServiceResponse<Dictionary<string, object>>.SuccessResponse(stats, "Statistics retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verification statistics");
                return ServiceResponse<Dictionary<string, object>>.InternalServerErrorResponse("Error getting statistics");
            }
        }

        public async Task<ServiceResponse<List<VerificationCode>>> GetUserCodesAsync(string userId, bool includeUsed = false)
        {
            try
            {
                var query = _context.VerificationCodes
                    .Where(vc => vc.UserId == userId);

                if (!includeUsed)
                {
                    query = query.Where(vc => !vc.IsUsed);
                }

                var userCodes = await query
                    .OrderByDescending(vc => vc.CreatedAt)
                    .ToListAsync();

                return ServiceResponse<List<VerificationCode>>.SuccessResponse(userCodes,
                    $"Found {userCodes.Count} verification codes for user");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting verification codes for user {UserId}", userId);
                return ServiceResponse<List<VerificationCode>>.InternalServerErrorResponse("Error getting user codes");
            }
        }

        #region Private Helper Methods

        private string GenerateCodeByType(VerificationCodeType type)
        {
            return type switch
            {
                VerificationCodeType.EmailVerification => GenerateNumericCode(6),
                VerificationCodeType.PhoneVerification => GenerateNumericCode(6),
                VerificationCodeType.PasswordReset => GenerateAlphanumericCode(6),
                VerificationCodeType.OtherVerification => GenerateNumericCode(6),
                _ => GenerateNumericCode(6)
            };
        }

        private int GetExpiryMinutesByType(VerificationCodeType type)
        {
            return type switch
            {
                VerificationCodeType.EmailVerification => 1440, // 24 hours
                VerificationCodeType.PhoneVerification => 10,   // 10 minutes
                VerificationCodeType.PasswordReset => 15,       // 15 minutes
                VerificationCodeType.OtherVerification => 60,   // 1 hour
                _ => 60
            };
        }

        private int GetMaxAttemptsByType(VerificationCodeType type)
        {
            return type switch
            {
                VerificationCodeType.EmailVerification => 3,
                VerificationCodeType.PhoneVerification => 3,
                VerificationCodeType.PasswordReset => 5,
                VerificationCodeType.OtherVerification => 3,
                _ => 3
            };
        }

        private string GenerateNumericCode(int length)
        {
            var random = new Random();
            var code = "";

            for (int i = 0; i < length; i++)
            {
                code += random.Next(0, 10).ToString();
            }

            return code;
        }

        private string GenerateAlphanumericCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var code = new char[length];

            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }

            return new string(code);
        }

        #endregion
    }
}