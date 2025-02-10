using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;
using SofteamDashboard.Server.Models.Requests;

namespace SofteamDashboard.Server.Endpoints.Cargos;

public class GetCargos : Endpoint<GetCargosRequest, IEnumerable<CargoDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetCargos(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Get("/api/cargos");
        Summary(s => 
        {
            s.Summary = "Retorna todos os cargos cadastrados.";
            s.Description = "Retorna todos os cargos cadastrados com varios niveis de aprofundamento.\n" +
                "Se o parametro 'includeFuncionarios' (bool) for informado, inclui os funcionários do cargo.";
            s.Params = new Dictionary<string, string>()
            {
                { "includeFuncionarios", "Inclui os funcionários do cargo." }
            };
            s.Responses[200] = "Cargos retornados com sucesso.";
        });
    }

    public override async Task HandleAsync(GetCargosRequest req, CancellationToken ct)
    {
        
        var query = _context.Cargos
            .Include(c => c.Permissoes)
            .ThenInclude(cp => cp.Permissao)
            .AsQueryable();
        
        if (req.IncludeFuncionarios!.Value)
        {
            query = query.Include(c => c.Funcionarios);
        }
        
        var cargos = await query
            .Skip(req.Page!.Value * req.PageSize!.Value).Take(req.PageSize!.Value).ToListAsync(ct);
        

        await SendOkAsync(cargos.Select(c => c.ToDto()), ct);
    }
    
}