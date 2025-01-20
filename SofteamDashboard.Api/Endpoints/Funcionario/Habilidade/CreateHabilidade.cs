using FastEndpoints;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Api.Endpoints.Funcionario.Habilidade;

public class CreateHabilidade : Endpoint<CreateHabilidadeRequest, HabilidadeFuncionarioDTO>
{
    private readonly SofteamDbContext _context;

    public CreateHabilidade(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Post("api/funcionarios/{id}/habilidades");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateHabilidadeRequest req, CancellationToken ct)
    {
        var funcionarioId = Route<int>("id");
        var habilidade = new HabilidadeFuncionario()
        {
            FuncionarioId = funcionarioId,
            NomeHabilidade = req.Nome,
            Nivel = req.Nivel,
            Verificado = false
        };

        _context.HabilidadeFuncionarios.Add(habilidade);
        await _context.SaveChangesAsync(ct);

        await SendCreatedAtAsync<GetFuncionarios>(null, habilidade.ToDto(), cancellation: ct);

    }
}