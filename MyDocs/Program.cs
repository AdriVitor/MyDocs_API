using FastEndpoints;
using FastEndpoints.Swagger;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MyDocs.Extensions;
using MyDocs.Infraestructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "MyDocs API";
        s.Version = "v1";
    };
});

builder.Services.ConfigureDependencyInjection();

builder.Services.ConfigureOptionsPattern(builder.Configuration);

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Context).Assembly.FullName));
});

builder.Services.ConfigureHangfire(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer(new BackgroundJobServerOptions
{
    Queues = new[] { "default", "alerts" }
});

app.UseFastEndpoints();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseAuthentication();
app.UseAuthorization();

app.Run();