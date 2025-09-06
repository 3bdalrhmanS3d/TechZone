using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmailService(
            IOptions<EmailSettings> emailSettings,
            ILogger<EmailService> logger,
            IWebHostEnvironment environment)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
            _environment = environment;
        }

        public async Task<ServiceResponse<bool>> SendEmailConfirmationAsync(string email, string confirmationLink)
        {
            try
            {
                var subject = "TechZone - Email Confirmation";
                var body = GetEmailConfirmationTemplate(confirmationLink);

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email confirmation to {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while sending confirmation email");
            }
        }

        public async Task<ServiceResponse<bool>> SendPasswordResetAsync(string email, string resetLink)
        {
            try
            {
                var subject = "TechZone - Password Reset";
                var body = GetPasswordResetTemplate(resetLink);

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending password reset email to {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while sending password reset email");
            }
        }

        public async Task<ServiceResponse<bool>> SendWelcomeEmailAsync(string email, string fullName)
        {
            try
            {
                var subject = "Welcome to TechZone!";
                var body = GetWelcomeTemplate(fullName);

                return await SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending welcome email to {Email}", email);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while sending welcome email");
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

                // In development mode, just log the email instead of sending
                if (_environment.IsDevelopment())
                {
                    _logger.LogInformation("Email would be sent to {Email} with subject: {Subject}", to, subject);
                    _logger.LogInformation("Email body: {Body}", body);
                    return ServiceResponse<bool>.SuccessResponse(true, "Email logged in development mode");
                }

                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
                emailMessage.To.Add(new MailboxAddress("", to));
                emailMessage.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };
                emailMessage.Body = builder.ToMessageBody();

                using var client = new SmtpClient();

                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email sent successfully to {Email}", to);
                return ServiceResponse<bool>.SuccessResponse(true, "Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Email}", to);
                return ServiceResponse<bool>.InternalServerErrorResponse(
                    "Error occurred while sending email");
            }
        }

        private string GetEmailConfirmationTemplate(string confirmationLink)
        {
            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Email Confirmation</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                        .button {{ background-color: #007bff; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #666; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Welcome to TechZone!</h2>
                        <p>Thank you for registering with TechZone. Please confirm your email address by clicking the button below:</p>
                        <a href='{confirmationLink}' class='button'>Confirm Email</a>
                        <p>If you didn't create an account with TechZone, please ignore this email.</p>
                        <div class='footer'>
                            <p>This is an automated email. Please do not reply.</p>
                            <p>&copy; 2025 TechZone. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }

        private string GetPasswordResetTemplate(string resetLink)
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
                        .button {{ background-color: #dc3545; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; display: inline-block; }}
                        .footer {{ margin-top: 30px; font-size: 12px; color: #666; }}
                        .warning {{ background-color: #fff3cd; border: 1px solid #ffeaa7; padding: 10px; border-radius: 5px; margin: 15px 0; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Password Reset Request</h2>
                        <p>We received a request to reset your password for your TechZone account.</p>
                        <div class='warning'>
                            <strong>Security Notice:</strong> This link will expire in 1 hour for your security.
                        </div>
                        <a href='{resetLink}' class='button'>Reset Password</a>
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
                            <p>You can now:</p>
                            <ul>
                                <li>Browse our extensive collection of laptops</li>
                                <li>Compare specifications and prices</li>
                                <li>Place orders securely</li>
                                <li>Track your orders in real-time</li>
                            </ul>
                            <p>Start exploring our products and find the perfect tech solution for your needs.</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; 2025 TechZone. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
        }
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
    }
}
