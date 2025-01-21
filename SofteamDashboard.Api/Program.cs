using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Application;

var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints();
bld.Services.AddDbContext<SofteamDbContext>(options =>
    options.UseSqlite("Data Source=softeam.db"));

// TEMPORARY
bld.Services.AddCors(o => o.AddPolicy("Any", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = bld.Build();

app.UseCors("Any");
app.UseFastEndpoints();
app.Run();