using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Projetos;

public class GetProjetos : EndpointWithoutRequest<IEnumerable<ProjetoDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetProjetos(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Get("api/projetos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var projetos = await _context.Projetos.ToListAsync();
        var projetosDto = projetos.Select(p => p.ToDto());

        await SendOkAsync(projetosDto, ct);
    }
}