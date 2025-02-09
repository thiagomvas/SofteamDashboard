using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class GetProjetos : EndpointWithoutRequest<IEnumerable<ProjetoDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetProjetos(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/projetos");
        Summary(s =>
        {
            s.Summary = "Retorna todos os projetos cadastrados.";
            s.Description = "Retorna todos os projetos cadastrados com varios niveis de aprofundamento.\n" +
                            "Se o parametro 'includeMembros' (bool) for informado, inclui os membros";
            s.Responses[200] = "Projetos retornados com sucesso.";
        });
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        bool includeMembros = Query<bool>("includeMembros", isRequired: false);
        
        IQueryable<Projeto> query = _context.Projetos;
        
        if (includeMembros)
        {
            query = query.Include(p => p.Membros);
        }
        
        var projetos = await query.ToListAsync(ct);
        
        await SendOkAsync(projetos.Select(p => p.ToDto()), ct);
    }
}