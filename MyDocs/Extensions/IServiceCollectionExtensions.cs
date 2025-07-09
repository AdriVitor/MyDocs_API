using FastEndpoints.Swagger;
using Hangfire;
using Microsoft.IdentityModel.Tokens;
using MyDocs.Features.Alerts.CreateAlert;
using MyDocs.Features.Alerts.DeleteAlert;
using MyDocs.Features.Alerts.GetAlertById;
using MyDocs.Features.Alerts.GetAlerts;
using MyDocs.Features.Alerts.UpdateAlert;
using MyDocs.Features.Authentication.Login;
using MyDocs.Features.Documents.DeleteDocument;
using MyDocs.Features.Documents.DownloadDocument;
using MyDocs.Features.Documents.GetDocumentById;
using MyDocs.Features.Documents.GetDocumentsByUser;
using MyDocs.Features.Documents.UploadDocument;
using MyDocs.Features.Users.Create;
using MyDocs.Features.Users.CreateUser;
using MyDocs.Features.Users.GetUser;
using MyDocs.Features.Users.UpdateUser;
using MyDocs.Infraestructure.ExternalServices.AzureBlob;
using MyDocs.Infraestructure.ExternalServices.Email;
using MyDocs.Infraestructure.ExternalServices.Hangfire;
using MyDocs.Settings;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Shared.Services.DocumentService;
using MyDocs.Shared.Services.EmailTemplateService;
using MyDocs.Shared.Services.ScheduleAlertService;
using MyDocs.Shared.Services.UserCredential;
using MyDocs.Shared.Services.UserService;
using NSwag;
using System.Text;

namespace MyDocs.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<IGetUserService, GetUserService>();
            services.AddScoped<IUpdateUserService, UpdateUserService>();

            services.AddScoped<ICreateAlertService, CreateAlertService>();
            services.AddScoped<IDeleteAlertService, DeleteAlertService>();
            services.AddScoped<IGetAlertByIdService, GetAlertByIdService>();
            services.AddScoped<IGetAlertsService, GetAlertsService>();
            services.AddScoped<IUpdateAlertService, UpdateAlertService>();

            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IDownloadDocumentService, DownloadDocumentService>();
            services.AddScoped<IGetDocumentByIdService, GetDocumentByIdService>();
            services.AddScoped<IGetDocumentsByUserService, GetDocumentsByUserService>();
            services.AddScoped<IDeleteDocumentService, DeleteDocumentService>();
            services.AddScoped<IUploadDocumentService, UploadDocumentService>();

            //Shared
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IScheduleJob, HangfireService>();
            services.AddTransient<IAzureBlobService, AzureBlobService>();
            services.AddTransient<IScheduleAlertService, ScheduleAlertService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            services.AddScoped<IUserCredentialService, UserCredentialService>();
            services.AddSingleton<IAzureBlobService, AzureBlobService>();

            return services;
        }

        public static IServiceCollection ConfigureOptionsPattern(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.Configure<AzureBlobStorageSettings>(configuration.GetSection("AzureBlobStorageSettings"));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            return services;
        }

        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
                config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfireServer(options =>
            {
                options.Queues = new[] { "default", "alerts" };
            });

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "MyDocs API";
                    s.Version = "v1";
                    s.AddAuth("Bearer", new()
                    {
                        Name = "Authorization",
                        Description = "Enter: your JWT token",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
                    });
                };

                o.EnableJWTBearerAuth = false;
            });

            return services;
        }
    }
}
