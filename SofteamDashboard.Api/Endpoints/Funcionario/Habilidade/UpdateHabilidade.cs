using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Funcionario.Habilidade;

public class UpdateHabilidade : Endpoint<UpdateHabilidadeRequest, HabilidadeFuncionarioDTO>
{
    private readonly SofteamDbContext _context;

    public UpdateHabilidade(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("api/funcionarios/{funcionarioId}/habilidades/{habilidadeId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateHabilidadeRequest req, CancellationToken ct)
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

        habilidade.NomeHabilidade = req.Nome ?? habilidade.NomeHabilidade;
        habilidade.Nivel = req.Nivel ?? habilidade.Nivel;

        _context.HabilidadeFuncionarios.Update(habilidade);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(habilidade.ToDto(), ct);
    }
}