using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SofteamDashboard.Application;
using SofteamDashboard.Application.Services;
using SofteamDashboard.Core.Abstractions;
using SofteamDashboard.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<SofteamDbContext>(provider =>
{
    var options = new DbContextOptionsBuilder<SofteamDbContext>()
        .UseInMemoryDatabase("softeam-db")
        .Options;
    return new SofteamDbContext(options);
});
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5276") }); // API url

await builder.Build().RunAsync();