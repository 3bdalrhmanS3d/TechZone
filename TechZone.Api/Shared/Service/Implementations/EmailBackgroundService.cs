using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TechZone.core.Service.Interfaces;
using TechZone.Domain.Entities;
using TechZone.Infrastructure.Application;
using TechZone.Shared.Service.Implementations;

namespace TechZone.Shared.Service.Implementations
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly ILogger<EmailBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

        public EmailBackgroundService(
            ILogger<EmailBackgroundService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email Background Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessEmailQueue();
                    await Task.Delay(_checkInterval, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing email queue.");
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Wait longer on error
                }
            }

            _logger.LogInformation("Email Background Service stopped.");
        }

        private async Task ProcessEmailQueue()
        {
            using var scope = _serviceProvider.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                // Get pending emails
                var pendingEmails = await emailService.GetPendingEmailsAsync(10);

                if (pendingEmails.Any())
                {
                    _logger.LogInformation("Processing {Count} pending emails.", pendingEmails.Count);

                    foreach (var email in pendingEmails)
                    {
                        try
                        {
                            // Mark as processing to avoid double processing
                            email.Status = EmailStatus.Retrying;
                            await context.SaveChangesAsync();

                            // Attempt to send email
                            var success = await emailService.SendEmailDirectlyAsync(email);

                            if (success)
                            {
                                await emailService.UpdateEmailStatusAsync(email.Id, EmailStatus.Sent);
                                _logger.LogInformation("Email sent successfully to {Email} (ID: {Id})",
                                    email.ToEmail, email.Id);
                            }
                            else
                            {
                                await emailService.UpdateEmailStatusAsync(email.Id, EmailStatus.Failed, "Failed to send email");
                                _logger.LogWarning("Failed to send email to {Email} (ID: {Id}). Retry count: {RetryCount}",
                                    email.ToEmail, email.Id, email.RetryCount + 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            await emailService.UpdateEmailStatusAsync(email.Id, EmailStatus.Failed, ex.Message);
                            _logger.LogError(ex, "Exception occurred while sending email to {Email} (ID: {Id})",
                                email.ToEmail, email.Id);
                        }
                    }
                }

                // Clean up old emails (older than 30 days)
                await CleanupOldEmails(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing email queue batch.");
            }
        }

        private async Task CleanupOldEmails(ApplicationDbContext context)
        {
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-30);

                var oldEmails = await context.EmailQueues
                    .Where(e => e.CreatedAt < cutoffDate &&
                               (e.Status == EmailStatus.Sent || e.Status == EmailStatus.Failed))
                    .Take(100)
                    .ToListAsync();

                if (oldEmails.Any())
                {
                    context.EmailQueues.RemoveRange(oldEmails);
                    await context.SaveChangesAsync();

                    _logger.LogInformation("Cleaned up {Count} old email records.", oldEmails.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cleaning up old emails.");
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email Background Service is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }

}