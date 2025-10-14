using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using System.Reflection;
using TechZone.core.Service.Interfaces;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;
using TechZone.Domain.Interfaces;
using TechZone.Domain.Service.Interfaces;
using TechZone.Features.Profile.Endpoints;
using TechZone.Infrastructure.Application;
using TechZone.Infrastructure.Repositories;
using TechZone.Infrastructure.UnitOfWork;
using TechZone.Services.Interfaces;
using TechZone.Shared.Data;
using TechZone.Shared.Service.Implementations;
using TechZoneV1.Features.Laptops.GetAllLaptops.Endpoints;

namespace TechZone
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Configure Serilog early
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "TechZone")
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} | {CorrelationId} | {Message:lj}{NewLine}{Exception}",
                    theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Literate)
                .WriteTo.File("logs/techzone-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {SourceContext} | {CorrelationId} | {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            try
            {
                Log.Information("🚀 Starting TechZone API application");

                var builder = WebApplication.CreateBuilder(args);
                // Load environment variables from .env
                DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

                // Initialize Cloudinary
                var cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
                cloudinary.Api.Secure = true;

                // Register Cloudinary as a service
                builder.Services.AddSingleton(cloudinary);


                // Replace default logging with Serilog
                builder.Host.UseSerilog();

                // Add services to the container.
                builder.Services.Configure<helper.JWT>(builder.Configuration.GetSection("JWT"));
                builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

                builder.Services.AddOptions<EmailSettings>()
                    .Bind(builder.Configuration.GetSection("EmailSettings"))
                    .ValidateDataAnnotations();

                // Add Correlation ID service
                builder.Services.AddScoped<ICorrelationIdService, CorrelationIdService>();

                // Services Registration
                builder.Services.AddScoped<IVerificationService, VerificationService>();
                builder.Services.AddScoped<IAuthService, AuthService>();
                builder.Services.AddScoped<IJwtService, JwtService>();
                builder.Services.AddScoped<IEmailService, EmailService>();
                builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
                builder.Services.AddScoped<ILaptopService, LaptopService>();
                builder.Services.AddScoped<ICategoryService, CategoryService>();
                builder.Services.AddScoped<IBrandService, BrandService>();

                // Background Services
                builder.Services.AddHostedService<EmailBackgroundService>();

                // Identity Configuration
                builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Password requirements
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;

                    // Email confirmation settings (disabled for now)
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

                // Database Configuration with reduced logging
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    // Try to read DATABASE_URL from Railway
                    //var dbUrl = null as string;
                    //"postgresql://postgres:GRgpDWQqsyUjfpcfZCYCvcmGiHCUTbGt@switchyard.proxy.rlwy.net:46456/railway";
                    string connectionString;

                    //if (!string.IsNullOrEmpty(dbUrl))
                    //{
                    //    // Convert postgres://... into Npgsql connection string
                    //    var uri = new Uri(dbUrl);
                    //    var userInfo = uri.UserInfo.Split(':');

                    //    connectionString =
                    //        $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};" +
                    //        $"Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
                    //}
                    //else
               //     {
                        // Local fallback (from appsettings.json)
                        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
             //       }

                    options.UseNpgsql(connectionString, npgsqlOptions =>
                    {
                        npgsqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorCodesToAdd: null
                        );
                        npgsqlOptions.CommandTimeout(30);
                    });

                    options.EnableSensitiveDataLogging(false);
                    options.EnableServiceProviderCaching();
                    options.EnableDetailedErrors(builder.Environment.IsDevelopment());

                    options.LogTo(message => Log.Debug("[EF] {Message}", message), LogLevel.Warning);
                });

                // mediator services for CQRS
                builder.Services.AddMediatR(typeof(Program).Assembly);
               // builder.Services.AddMediatR(typeof(TechZone.EF.Features.Profile.Queries.GetProfileQueryHandler).Assembly);

                //builder.Services.AddScoped<IBaseRepository<LaptopPort>, BaseRepository<LaptopPort>>();

                // Dynamic generic repositories for BaseEntity subclasses
                var baseEntityAssembly = Assembly.GetAssembly(typeof(BaseEntity)) ?? Assembly.GetExecutingAssembly();  // Fallback if null
                var entityTypes = baseEntityAssembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseEntity)))
                    .ToList();

                foreach (var entityType in entityTypes)
                {
                    var interfaceType = typeof(IBaseRepository<>).MakeGenericType(entityType);
                    var implementationType = typeof(BaseRepository<>).MakeGenericType(entityType);
                    builder.Services.AddScoped(interfaceType, implementationType);
                }


                builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });

                // Authentication Configuration
                builder.Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = "Bearer";
                    option.DefaultChallengeScheme = "Bearer";
                    option.DefaultScheme = "Bearer";
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

                // Authorization Policies
                builder.Services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                    options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
                });

                // Controllers
                builder.Services.AddControllers()
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressModelStateInvalidFilter = false;
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                        options.JsonSerializerOptions.WriteIndented = true;
                    });

                // API Documentation
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "TechZone API",
                        Version = "v1",
                        Description = "A comprehensive e-commerce API for technology products",
                        Contact = new OpenApiContact
                        {
                            Name = "TechZone Support",
                            Email = "support@techzone.com"
                        }
                    });

                    // JWT Authentication in Swagger
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = @"JWT Authorization header using the Bearer scheme. 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          Example: 'Bearer 12345abcdef'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                    // Include XML comments if available
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                });


                // CORS Configuration
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigins",
                        policy =>
                        {
                            policy.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:4200", "https://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials();
                        });
                });

                var app = builder.Build();

                // Add Correlation ID middleware
                app.UseMiddleware<CorrelationIdMiddleware>();

                // Database Migration and Seeding
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var correlationId = services.GetRequiredService<ICorrelationIdService>();

                    try
                    {
                        Log.Information("📊 Starting database migration and seeding...");

                        var context = services.GetRequiredService<ApplicationDbContext>();
                        await context.Database.MigrateAsync();
                        Log.Information("✅ Database migration completed successfully");

                        await DatabaseSeeder.SeedAsync(services);
                        Log.Information("🌱 Database seeding completed successfully");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "❌ An error occurred while migrating or seeding the database");

                        if (app.Environment.IsDevelopment())
                        {
                            throw;
                        }

                        Log.Warning("⚠️ Application will continue without seeding");
                    }
                }

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechZone API V1");
                        c.RoutePrefix = "swagger";
                        c.DocumentTitle = "TechZone API Documentation";
                        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                        c.DefaultModelsExpandDepth(-1);
                    });

                    // Redirect root to swagger in development
                    app.MapGet("/", () => Results.Redirect("/swagger"));
                }
                else
                {
                    app.UseHsts();

                    // Simple health check for production
                    app.MapGet("/", () => Results.Ok(new
                    {
                        message = "TechZone API is running",
                        environment = app.Environment.EnvironmentName,
                        timestamp = DateTime.UtcNow
                    }));
                }

                app.UseHttpsRedirection();

                // Security Headers
                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                    context.Response.Headers.Add("X-Frame-Options", "DENY");
                    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                    await next();
                });
                app.MapProfileEndpoint();
                app.MapGetAllLaptopEndpoint();
                app.UseCors("AllowSpecificOrigins");
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                // Logging startup information
                Log.Information("🌟 TechZone API started successfully");
                Log.Information("🔧 Environment: {Environment}", app.Environment.EnvironmentName);

                if (app.Environment.IsDevelopment())
                {
                    Log.Information("📚 Swagger documentation available at: /swagger");
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "💥 Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }

    // Correlation ID Service Interface
    public interface ICorrelationIdService
    {
        string CorrelationId { get; }
        void SetCorrelationId(string correlationId);
    }

    // Correlation ID Service Implementation
    public class CorrelationIdService : ICorrelationIdService
    {
        private readonly AsyncLocal<string> _correlationId = new();

        public string CorrelationId => _correlationId.Value ?? "N/A";

        public void SetCorrelationId(string correlationId)
        {
            _correlationId.Value = correlationId;
        }
    }

    // Correlation ID Middleware
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeaderName = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICorrelationIdService correlationIdService)
        {
            var correlationId = GetCorrelationId(context);
            correlationIdService.SetCorrelationId(correlationId);

            // Add correlation ID to response headers
            context.Response.Headers.Add(CorrelationIdHeaderName, correlationId);

            // Add to Serilog context
            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }

        private static string GetCorrelationId(HttpContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault();
            return string.IsNullOrEmpty(correlationId) ? Guid.NewGuid().ToString("N")[..8] : correlationId;
        }
    }
}