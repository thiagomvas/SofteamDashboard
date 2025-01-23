using FastEndpoints;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Api.Endpoints.Projetos;

public class CreateProjeto : Endpoint<CreateProjetoRequest, ProjetoDTO>
{
    private readonly SofteamDbContext _context;

    public CreateProjeto(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Post("api/projetos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProjetoRequest req, CancellationToken ct)
    {
        var projeto = new Projeto()
        {
            Titulo = req.Titulo,
            Descricao = req.Descricao,
            Inicio = req.Inicio,
            Fim = req.Fim
        };

        var entity = _context.Projetos.Add(projeto);
        await _context.SaveChangesAsync(ct);
        
         

        await SendOkAsync(entity.Entity.ToDto(), ct);
    }
}