using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;
using SofteamDashboard.Api.Extensions;

namespace SofteamDashboard.Api.Endpoints.Funcionario;

public class GetFuncionarios : EndpointWithoutRequest<IEnumerable<FuncionarioDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetFuncionarios(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Get("/api/funcionarios");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var funcionarios = await _context.Funcionarios
            .Include(f => f.Habilidades)
            .ToListAsync();
        var dtos = funcionarios.Select(f => f.ToDto());

        await SendOkAsync(dtos, ct);
    }
}