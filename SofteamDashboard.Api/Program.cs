using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
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

bld.Services.Configure<JsonOptions>(o =>
{
    o.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    o.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

});

bld.Services.SwaggerDocument();

var app = bld.Build();

app.UseCors("Any");
app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();