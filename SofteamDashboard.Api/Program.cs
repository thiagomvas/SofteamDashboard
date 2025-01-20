using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Application;

var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints();
bld.Services.AddDbContext<SofteamDbContext>(options =>
    options.UseSqlite("Data Source=softeam.db"));

var app = bld.Build();

app.UseFastEndpoints();
app.Run();