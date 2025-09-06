using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Services.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Send email confirmation link
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="confirmationLink">Confirmation link</param>
        /// <returns>Service response with send result</returns>
        Task<ServiceResponse<bool>> SendEmailConfirmationAsync(string email, string confirmationLink);

        /// <summary>
        /// Send password reset link
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="resetLink">Password reset link</param>
        /// <returns>Service response with send result</returns>
        Task<ServiceResponse<bool>> SendPasswordResetAsync(string email, string resetLink);

        /// <summary>
        /// Send welcome email after successful registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="fullName">User full name</param>
        /// <returns>Service response with send result</returns>
        Task<ServiceResponse<bool>> SendWelcomeEmailAsync(string email, string fullName);

        /// <summary>
        /// Send general email
        /// </summary>
        /// <param name="to">Recipient email</param>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body (HTML)</param>
        /// <returns>Service response with send result</returns>
        Task<ServiceResponse<bool>> SendEmailAsync(string to, string subject, string body);
    }
}
