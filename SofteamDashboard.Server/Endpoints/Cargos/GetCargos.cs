using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Cargos;

public class GetCargos : EndpointWithoutRequest<IEnumerable<CargoDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetCargos(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Get("/api/cargos");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        bool includeFuncionarios = Query<bool>("includeFuncionarios", isRequired: false);
        
        var query = _context.Cargos
            .Include(c => c.Permissoes)
            .ThenInclude(cp => cp.Permissao)
            .AsQueryable();
        
        if (includeFuncionarios)
        {
            query = query.Include(c => c.Funcionarios);
        }
        
        var cargos = await query.ToListAsync(ct);
        

        await SendOkAsync(cargos.Select(c => c.ToDto()), ct);
    }
    
}