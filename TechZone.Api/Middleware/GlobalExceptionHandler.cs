using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ServiceResponse<object>();

            switch (exception)
            {
                case KeyNotFoundException:
                    response = ServiceResponse<object>.NotFoundResponse(
                        exception.Message,
                        "المورد المطلوب غير موجود"
                    );
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException:
                    response = ServiceResponse<object>.UnauthorizedResponse(
                        "Unauthorized access",
                        "وصول غير مصرح به"
                    );
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case ArgumentException:
                    response = ServiceResponse<object>.ErrorResponse(
                        exception.Message,
                        "خطأ في البيانات المدخلة",
                        400
                    );
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidOperationException:
                    response = ServiceResponse<object>.ErrorResponse(
                        exception.Message,
                        "عملية غير صالحة",
                        400
                    );
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    if (_environment.IsDevelopment())
                    {
                        response = ServiceResponse<object>.InternalServerErrorResponse(
                            $"Internal server error: {exception.Message}",
                            "خطأ في الخادم الداخلي"
                        );
                    }
                    else
                    {
                        response = ServiceResponse<object>.InternalServerErrorResponse(
                            "An internal server error occurred",
                            "حدث خطأ في الخادم الداخلي"
                        );
                    }
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // Include stack trace only in development
            if (_environment.IsDevelopment() && exception != null)
            {
                response.Errors.Add($"Stack Trace: {exception.StackTrace}");

                if (exception.InnerException != null)
                {
                    response.Errors.Add($"Inner Exception: {exception.InnerException.Message}");
                }
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    // Extension method to register the middleware
    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}