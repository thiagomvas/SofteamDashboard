using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server;
using SofteamDashboard.Server.Middlewares;
using SofteamDashboard.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(s => s.SigningKey = builder.Configuration["Jwt:Key"])
    .AddAuthorization();

builder.Services.AddDbContext<SofteamDbContext>(o => o.UseInMemoryDatabase("softeamdb"));

builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.Title = "Softeam Server API";
        s.Version = "v1";
    };
    o.AutoTagPathSegmentIndex = 2;
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SeedService>();

// Set the port number to 8080
var url = builder.Configuration["Url"];
builder.WebHost.UseUrls(url ?? throw new ArgumentNullException("Url"));


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
    await seedService.SeedAsync();
}

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();

app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();