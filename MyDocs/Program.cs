using FastEndpoints;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MyDocs.Extensions;
using MyDocs.Infraestructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureDependencyInjection();

builder.Services.ConfigureOptionsPattern(builder.Configuration);

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(Context).Assembly.FullName));
});

builder.Services.ConfigureHangfire(builder.Configuration);

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer(new BackgroundJobServerOptions
{
    Queues = new[] { "default", "alerts" }
});

app.UseAuthentication();
app.UseAuthorization();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseFastEndpoints();

app.Run();