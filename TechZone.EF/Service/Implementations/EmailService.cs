using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text.Json;
using TechZone.core.Service.Interfaces;
using TechZone.Core.Entities;
using TechZone.Core.Entities;
using TechZone.Core.Entities.User;
using TechZone.Core.ServiceResponse;
using TechZone.EF.Application;

namespace TechZone.EF.Service.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EmailService> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public EmailService(
            UserManager<ApplicationUser> userManager,
            IOptions<EmailSettings> emailSettings,
            ILogger<EmailService> logger,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _emailSettings = emailSettings.Value;
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _environment = environment;

            
        }

        public async Task<ServiceResponse<bool>> SendEmailConfirmationAsync(string email, string? confirmationCode = null)
        {
            try
            {
                // Get user details to get the name
                var user = await _userManager.FindByEmailAsync(email);
                string toName = user?.FullName ?? "User";

                var code = confirmationCode ?? GenerateVerificationCode();
                var subject = "Confirm Your Email - TechZone";
                var body = GetEmailConfirmationTemplate(code);

                await QueueEmailAsync(email, subject, body, EmailType.Verification,
                    EmailPriority.High, new { Code = code }, true, null, toName);

                return ServiceResponse<bool>.SuccessResponse(true, "Confirmation email sent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email confirmation to {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse("Failed to send confirmation email");
            }
        }

        public async Task<ServiceResponse<bool>> SendPasswordResetAsync(string email, string resetCode)
        {
            try
            {
                var subject = "TechZone - Password Reset Code";
                var body = GetPasswordResetTemplate(resetCode);

                await QueueEmailAsync(
                    email,
                    subject,
                    body,
                    EmailType.PasswordReset,
                    EmailPriority.High,
                    new { ResetCode = resetCode }
                );

                _logger.LogInformation("Password reset email queued for: {Email}", email);
                return ServiceResponse<bool>.SuccessResponse(true, "Password reset email queued successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error queuing password reset email for {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while queuing password reset email");
            }
        }

        public async Task<ServiceResponse<bool>> SendWelcomeEmailAsync(string email, string fullName)
        {
            try
            {
                var subject = "Welcome to TechZone!";
                var body = GetWelcomeTemplate(fullName);

                await QueueEmailAsync(
                    email,
                    subject,
                    body,
                    EmailType.Welcome,
                    EmailPriority.Normal,
                    new { UserName = fullName }
                );

                _logger.LogInformation("Welcome email queued for: {Email}", email);
                return ServiceResponse<bool>.SuccessResponse(true, "Welcome email queued successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error queuing welcome email for {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while queuing welcome email");
            }
        }

        public async Task<ServiceResponse<bool>> SendNotificationEmailAsync(string email, string subject, string message, bool isHtml = true)
        {
            try
            {
                await QueueEmailAsync(
                    email,
                    subject,
                    message,
                    EmailType.Notification,
                    EmailPriority.Normal,
                    new { Message = message },
                    isHtml
                );

                _logger.LogInformation("Notification email queued for: {Email}", email);
                return ServiceResponse<bool>.SuccessResponse(true, "Notification email queued successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error queuing notification email for {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while queuing notification email");
            }
        }

        public async Task<ServiceResponse<bool>> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(to))
                {
                    return ServiceResponse<bool>.ErrorResponse(
                        "Recipient email is required",
                        "البريد الإلكتروني للمستلم مطلوب",
                        400);
                }

                // Queue email instead of sending directly
                await QueueEmailAsync(to, subject, body, EmailType.Notification);

                _logger.LogInformation("Email queued for: {Email}", to);
                return ServiceResponse<bool>.SuccessResponse(true, "Email queued successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error queuing email for {Email}", to);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while queuing email");
            }
        }

        private async Task QueueEmailAsync(string toEmail, string subject, string body,
        EmailType emailType, EmailPriority priority = EmailPriority.Normal,
        object? templateData = null, bool isHtml = true, DateTime? scheduledAt = null,
        string? toName = null)
        {
            var emailQueue = new EmailQueue
            {
                Id = Guid.NewGuid(),
                ToEmail = toEmail,
                Subject = subject,
                ToName = toName ?? string.Empty,
                Body = body,
                IsHtml = isHtml,
                EmailType = emailType,
                Priority = priority,
                ScheduledAt = scheduledAt ?? DateTime.UtcNow,
                TemplateData = templateData != null ? JsonSerializer.Serialize(templateData) : string.Empty,
                CreatedAt = DateTime.UtcNow,
                Status = EmailStatus.Pending,
                ErrorMessage = string.Empty,
                MaxRetries = 3, 
                RetryCount = 0
            };

            await _context.EmailQueues.AddAsync(emailQueue);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EmailQueue>> GetPendingEmailsAsync(int batchSize = 10)
        {
            return await _context.EmailQueues
                .Where(e => e.Status == EmailStatus.Pending &&
                           (e.ScheduledAt == null || e.ScheduledAt <= DateTime.UtcNow) &&
                           (e.NextRetryAt == null || e.NextRetryAt <= DateTime.UtcNow) &&
                           e.RetryCount < e.MaxRetries)
                .OrderBy(e => e.Priority == EmailPriority.High ? 1 : e.Priority == EmailPriority.Normal ? 2 : 3)
                .ThenBy(e => e.CreatedAt)
                .Take(batchSize)
                .ToListAsync();
        }

        public async Task<bool> SendEmailDirectlyAsync(EmailQueue emailItem)
        {
            try
            {
                _logger.LogInformation("Starting email send process for {Email}", emailItem.ToEmail);

                // Log email settings for debugging
                _logger.LogInformation("Current Email Settings: Server={Server}, Port={Port}, Username={Username}, EnableSsl={EnableSsl}",
                    _emailSettings.SmtpServer, _emailSettings.Port, _emailSettings.Username, _emailSettings.EnableSsl);

                // Check if email settings are properly configured
                if (string.IsNullOrEmpty(_emailSettings.SmtpServer) || string.IsNullOrEmpty(_emailSettings.Username))
                {
                    _logger.LogError("Email configuration is incomplete. SmtpServer: {Server}, Username: {Username}",
                        _emailSettings.SmtpServer ?? "NULL", _emailSettings.Username ?? "NULL");
                    return false;
                }

                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(
                    _emailSettings.FromName ?? "TechZone",
                    _emailSettings.FromEmail));

                emailMessage.To.Add(new MailboxAddress(emailItem.ToName ?? "", emailItem.ToEmail));
                emailMessage.Subject = emailItem.Subject;

                var builder = new BodyBuilder();
                if (emailItem.IsHtml)
                {
                    builder.HtmlBody = emailItem.Body;
                }
                else
                {
                    builder.TextBody = emailItem.Body;
                }
                emailMessage.Body = builder.ToMessageBody();

                _logger.LogInformation("Email message created successfully");

                using var client = new SmtpClient();

                try
                {
                    _logger.LogInformation("Attempting to connect to SMTP server: {Server}:{Port}",
                        _emailSettings.SmtpServer, _emailSettings.Port);

                    // استخدام الإعدادات الصحيحة لـ Yahoo Mail
                    SecureSocketOptions secureSocketOptions;

                    if (_emailSettings.EnableSsl)
                    {
                        if (_emailSettings.Port == 465)
                        {
                            secureSocketOptions = SecureSocketOptions.SslOnConnect;
                            _logger.LogInformation("Using SSL on Connect (Port 465)");
                        }
                        else if (_emailSettings.Port == 587)
                        {
                            secureSocketOptions = SecureSocketOptions.StartTls;
                            _logger.LogInformation("Using StartTLS (Port 587)");
                        }
                        else
                        {
                            secureSocketOptions = SecureSocketOptions.Auto;
                            _logger.LogInformation("Using Auto SSL detection");
                        }
                    }
                    else
                    {
                        secureSocketOptions = SecureSocketOptions.None;
                        _logger.LogInformation("SSL disabled");
                    }

                    await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, secureSocketOptions);
                    _logger.LogInformation("Connected to SMTP server successfully");

                    _logger.LogInformation("Attempting to authenticate with username: {Username}", _emailSettings.Username);
                    await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                    _logger.LogInformation("Authentication successful");

                    _logger.LogInformation("Attempting to send email...");
                    var result = await client.SendAsync(emailMessage);
                    _logger.LogInformation("Email send result: {Result}", result);

                    await client.DisconnectAsync(true);
                    _logger.LogInformation("Disconnected from SMTP server");

                    _logger.LogInformation("Email sent successfully to: {Email}", emailItem.ToEmail);
                    return true;
                }
                catch (Exception smtpEx)
                {
                    _logger.LogError(smtpEx, "SMTP specific error occurred while sending email to {Email}. Error: {ErrorMessage}",
                        emailItem.ToEmail, smtpEx.Message);

                    // Log specific SMTP error details
                    if (smtpEx.Message.Contains("authentication") || smtpEx.Message.Contains("Invalid user name or password"))
                    {
                        _logger.LogError("Authentication failed - Verify Yahoo App Password. Current username: {Username}", _emailSettings.Username);
                    }
                    else if (smtpEx.Message.Contains("connection") || smtpEx.Message.Contains("unable to connect"))
                    {
                        _logger.LogError("Connection failed - Server: {Server}, Port: {Port}", _emailSettings.SmtpServer, _emailSettings.Port);
                    }
                    else if (smtpEx.Message.Contains("SSL") || smtpEx.Message.Contains("TLS"))
                    {
                        _logger.LogError("SSL/TLS error - EnableSsl: {EnableSsl}, Port: {Port}", _emailSettings.EnableSsl, _emailSettings.Port);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error occurred while sending email to {Email}. Error: {ErrorMessage}",
                    emailItem.ToEmail, ex.Message);
                return false;
            }
        }

        public async Task UpdateEmailStatusAsync(Guid emailId, EmailStatus status, string? errorMessage = null)
        {
            var email = await _context.EmailQueues.FindAsync(emailId);
            if (email != null)
            {
                email.Status = status;
                email.ErrorMessage = errorMessage;

                if (status == EmailStatus.Sent)
                {
                    email.SentAt = DateTime.UtcNow;
                }
                else if (status == EmailStatus.Failed)
                {
                    email.RetryCount++;
                    if (email.RetryCount < email.MaxRetries)
                    {
                        email.Status = EmailStatus.Pending;
                        email.NextRetryAt = DateTime.UtcNow.AddMinutes(Math.Pow(2, email.RetryCount)); // Exponential backoff
                    }
                    else
                    {
                        email.Status = EmailStatus.Failed;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        // Updated method signature to accept string for backward compatibility
        public async Task UpdateEmailStatusAsync(Guid emailId, string status, string? errorMessage = null)
        {
            if (Enum.TryParse<EmailStatus>(status, out var emailStatus))
            {
                await UpdateEmailStatusAsync(emailId, emailStatus, errorMessage);
            }
        }

        private string GetEmailConfirmationTemplate(string confirmationCode)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Email Verification</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .code-box {{ background-color: #f8f9fa; border: 2px solid #007bff; padding: 20px; text-align: center; font-size: 32px; font-weight: bold; color: #007bff; border-radius: 10px; margin: 20px 0; letter-spacing: 3px; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #666; }}
                        .warning {{ background-color: #fff3cd; border: 1px solid #ffeaa7; padding: 10px; border-radius: 5px; margin: 15px 0; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>TechZone - Email Verification</h2>
                        <p>Thank you for registering with TechZone. Please use the verification code below to confirm your email address:</p>
                        
                        <div class='code-box'>
                            {confirmationCode}
                        </div>
                        
                        <div class='warning'>
                            <strong>Important:</strong> This verification code will expire in 24 hours.
                        </div>
                        
                        <p>Enter this code in the verification form to complete your registration.</p>
                        <p>If you didn't create an account with TechZone, please ignore this email.</p>
                        
                        <div class='footer'>
                            <p>This is an automated email. Please do not reply.</p>
                            <p>&copy; 2025 TechZone. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        private string GetPasswordResetTemplate(string resetCode)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Password Reset</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .code-box {{ background-color: #fff5f5; border: 2px solid #dc3545; padding: 20px; text-align: center; font-size: 32px; font-weight: bold; color: #dc3545; border-radius: 10px; margin: 20px 0; letter-spacing: 3px; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #666; }}
                        .warning {{ background-color: #f8d7da; border: 1px solid #f5c6cb; padding: 10px; border-radius: 5px; margin: 15px 0; color: #721c24; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>TechZone - Password Reset Request</h2>
                        <p>We received a request to reset your password for your TechZone account.</p>
                        <p>Use the following code to reset your password:</p>
                        
                        <div class='code-box'>
                            {resetCode}
                        </div>
                        
                        <div class='warning'>
                            <strong>Security Notice:</strong> This code will expire in 15 minutes for your security.
                        </div>
                        
                        <p>Enter this code in the password reset form to continue.</p>
                        <p>If you didn't request a password reset, please ignore this email and your password will remain unchanged.</p>
                        
                        <div class='footer'>
                            <p>This is an automated email. Please do not reply.</p>
                            <p>&copy; 2025 TechZone. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        private string GetWelcomeTemplate(string fullName)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Welcome to TechZone</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .header {{ background-color: #007bff; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                        .content {{ background-color: #f8f9fa; padding: 20px; border-radius: 0 0 5px 5px; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #666; text-align: center; }}
                        .features {{ background-color: white; padding: 15px; border-radius: 5px; margin: 15px 0; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Welcome to TechZone!</h1>
                        </div>
                        <div class='content'>
                            <h2>Hello {fullName}!</h2>
                            <p>Thank you for joining TechZone, your ultimate destination for the latest technology products!</p>
                            
                            <div class='features'>
                                <h3>You can now:</h3>
                                <ul>
                                    <li>Browse our extensive collection of laptops</li>
                                    <li>Compare specifications and prices</li>
                                    <li>Place orders securely</li>
                                    <li>Track your orders in real-time</li>
                                    <li>Get exclusive member discounts</li>
                                </ul>
                            </div>
                            
                            <p>Start exploring our products and find the perfect tech solution for your needs.</p>
                            <p>Happy shopping!</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; 2025 TechZone. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        #region Code Generation Methods

        /// <summary>
        /// Generates a 6-digit numeric verification code for email confirmation
        /// </summary>
        /// <returns>6-digit numeric code as string</returns>
        private string GenerateVerificationCode()
        {
            return GenerateSecureCode(6, false);
        }

        
        /// <summary>
        /// Generates a cryptographically secure random code
        /// </summary>
        /// <param name="length">Length of the code</param>
        /// <param name="includeLetters">Include letters (A-Z, a-z) in addition to numbers</param>
        /// <returns>Secure random code as string</returns>
        private string GenerateSecureCode(int length, bool includeLetters = false)
        {
            const string numbers = "0123456789";
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var chars = numbers;
            if (includeLetters)
            {
                chars += letters;
            }

            var code = new char[length];
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }

            return new string(code);
        }

        #endregion

    }

    // Configuration class for email settings
    public class EmailSettings
    {
        public string FromName { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = false;
        public bool EnableInDevelopment { get; set; } = false;
    }
}