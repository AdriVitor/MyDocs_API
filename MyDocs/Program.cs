using FastEndpoints;
using FastEndpoints.Swagger;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
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
using MyDocs.Infraestructure.Persistence;
using MyDocs.Infraestructure.Services.ProcessAlerts;
using MyDocs.Infraestructure.Services.ScheduleAlertService;
using MyDocs.Settings;
using MyDocs.Shared.Services.AlertService;
using MyDocs.Shared.Services.DocumentService;
using MyDocs.Shared.Services.EmailTemplateService;
using MyDocs.Shared.Services.UserService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();


///////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddFastEndpoints().AddSwaggerDocument();

builder.Services.AddScoped<ICreateUserService, CreateUserService>();
builder.Services.AddScoped<IGetUserService, GetUserService>();
builder.Services.AddScoped<IUpdateUserService, UpdateUserService>();

builder.Services.AddScoped<ICreateAlertService, CreateAlertService>();
builder.Services.AddScoped<IDeleteAlertService, DeleteAlertService>();
builder.Services.AddScoped<IGetAlertByIdService, GetAlertByIdService>();
builder.Services.AddScoped<IGetAlertsService, GetAlertsService>();
builder.Services.AddScoped<IUpdateAlertService, UpdateAlertService>();

builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IDownloadDocumentService, DownloadDocumentService>();
builder.Services.AddScoped<IGetDocumentByIdService, GetDocumentByIdService>();
builder.Services.AddScoped<IGetDocumentsByUserService, GetDocumentsByUserService>();
builder.Services.AddScoped<IDeleteDocumentService, DeleteDocumentService>();
builder.Services.AddScoped<IUploadDocumentService, UploadDocumentService>();


builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IScheduleJob, HangfireService>();
builder.Services.AddTransient<IAzureBlobService, AzureBlobService>();
builder.Services.AddTransient<IScheduleAlertService, ScheduleAlertService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddSingleton<IAzureBlobService, AzureBlobService>();

///////////////////////////////////////////////////////////////////////////////////////////////////

//Options Pattern
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.Configure<AzureBlobStorageSettings>(builder.Configuration.GetSection("AzureBlobStorageSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

///////////////////////////////////////////////////////////////////////////////////////////////////

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
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

builder.Services.AddAuthorization();

///////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Context).Assembly.FullName));
});


///////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer(options =>
{
    options.Queues = new[] { "alerts" };
});

///////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // Ajuste conforme sua necessidade
});

///////////////////////////////////////////////////////////////////////////////////////////////////

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

///////////////////////////////////////////////////////////////////////////////////////////////////

app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer(new BackgroundJobServerOptions
{
    Queues = new[] { "alerts" }
});


///////////////////////////////////////////////////////////////////////////////////////////////////
app.UseFastEndpoints();

app.UseOpenApi();
app.UseSwaggerUi();

///////////////////////////////////////////////////////////////////////////////////////////////////


app.UseAuthentication();
app.UseAuthorization();

//app.MapControllers();

app.Run();