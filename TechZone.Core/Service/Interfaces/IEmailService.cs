using TechZone.Core.Entities;
using TechZone.Core.ServiceResponse;

namespace TechZone.core.Service.Interfaces
{
    public interface IEmailService
    {
        Task<ServiceResponse<bool>> SendEmailConfirmationAsync(string email, string? confirmationCode = null);
        Task<ServiceResponse<bool>> SendPasswordResetAsync(string email, string? resetCode = null);
        Task<ServiceResponse<bool>> SendWelcomeEmailAsync(string email, string fullName);

        // Background service methods
        Task<List<EmailQueue>> GetPendingEmailsAsync(int batchSize = 10);
        Task<bool> SendEmailDirectlyAsync(EmailQueue emailItem);
        Task UpdateEmailStatusAsync(Guid emailId, EmailStatus status, string? errorMessage = null);
        Task UpdateEmailStatusAsync(Guid emailId, string status, string? errorMessage = null); // Backward compatibility
    }
}