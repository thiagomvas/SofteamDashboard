using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Funcionario.Habilidade;

public class DeleteHabilidade : EndpointWithoutRequest
{
    private readonly SofteamDbContext _context;

    public DeleteHabilidade(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Delete("api/funcionarios/{funcionarioId}/habilidades/{habilidadeId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var funcionarioId = Route<int>("funcionarioId");
        var habilidadeId = Route<int>("habilidadeId");
        var habilidade = await _context.HabilidadeFuncionarios
            .FirstOrDefaultAsync(h => h.Id == habilidadeId && h.FuncionarioId == funcionarioId, cancellationToken: ct);

        if (habilidade is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        _context.HabilidadeFuncionarios.Remove(habilidade);
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);

    }
}