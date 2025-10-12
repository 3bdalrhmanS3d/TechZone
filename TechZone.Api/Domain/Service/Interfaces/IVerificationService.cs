using TechZone.Domain.Entities;
using TechZone.Domain.ServiceResponse;

namespace TechZone.Domain.Service.Interfaces
{
    public interface IVerificationService
    {
        /// <summary>
        /// Creates a new verification code for a user
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="type">Type of verification code</param>
        /// <param name="destination">Where the code will be sent (Email, Phone, Both)</param>
        /// <param name="customCode">Optional custom code, if null a random code will be generated</param>
        /// <returns>ServiceResponse with the created VerificationCode</returns>
        Task<ServiceResponse<VerificationCode>> CreateVerificationCodeAsync(
            string userId, 
            VerificationCodeType type, 
            DestinationStatus destination, 
            string? customCode = null);

        /// <summary>
        /// Verifies a code and marks it as used if valid
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="code">The verification code to verify</param>
        /// <param name="type">Type of verification code</param>
        /// <returns>ServiceResponse indicating if verification was successful</returns>
        Task<ServiceResponse<bool>> VerifyCodeAsync(string userId, string code, VerificationCodeType type);

        /// <summary>
        /// Checks if a code is valid without marking it as used
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="code">The verification code to check</param>
        /// <param name="type">Type of verification code</param>
        /// <returns>ServiceResponse indicating if code is valid</returns>
        Task<ServiceResponse<bool>> IsCodeValidAsync(string userId, string code, VerificationCodeType type);

        /// <summary>
        /// Invalidates all active codes of a specific type for a user
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="type">Type of verification codes to invalidate</param>
        /// <returns>ServiceResponse indicating if invalidation was successful</returns>
        Task<ServiceResponse<bool>> InvalidateUserCodesAsync(string userId, VerificationCodeType type);

        /// <summary>
        /// Removes expired verification codes from the database
        /// </summary>
        /// <returns>ServiceResponse with count of cleaned up codes</returns>
        Task<ServiceResponse<bool>> CleanupExpiredCodesAsync();

        /// <summary>
        /// Gets the active verification code for a user of specific type
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="type">Type of verification code</param>
        /// <returns>ServiceResponse with the active VerificationCode or null if none found</returns>
        Task<ServiceResponse<VerificationCode?>> GetActiveCodeAsync(string userId, VerificationCodeType type);

        /// <summary>
        /// Gets verification code statistics for admin dashboard
        /// </summary>
        /// <returns>ServiceResponse with verification code statistics</returns>
        Task<ServiceResponse<Dictionary<string, object>>> GetVerificationStatsAsync();

        /// <summary>
        /// Gets verification codes for a specific user (admin use)
        /// </summary>
        /// <param name="userId">The user ID</param>
        /// <param name="includeUsed">Whether to include used codes</param>
        /// <returns>ServiceResponse with list of user's verification codes</returns>
        Task<ServiceResponse<List<VerificationCode>>> GetUserCodesAsync(string userId, bool includeUsed = false);
    }
}