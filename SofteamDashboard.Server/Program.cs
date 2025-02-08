using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server;

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
});

builder.Services.AddScoped<AuthService>();

// Set the port number to 8080
var url = builder.Configuration["Url"];
builder.WebHost.UseUrls(url ?? throw new ArgumentNullException("Url"));


var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints()
    .UseSwaggerGen();
app.Run();