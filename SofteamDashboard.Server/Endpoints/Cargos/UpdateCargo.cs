using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Cargos;

public class UpdateCargo : Endpoint<UpdateCargoRequest, CargoDTO>
{
    private readonly SofteamDbContext _context;
    
    public UpdateCargo(SofteamDbContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Put("/api/cargos/{id}");
        Permissions(Constants.ADMIN);
    }
    
    public override async Task HandleAsync(UpdateCargoRequest req, CancellationToken ct)
    {
        int id = Route<int>("id");
        
        var cargo = await _context.Cargos
            .Include(c => c.Permissoes)
            .ThenInclude(cp => cp.Permissao)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (cargo == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        cargo.Nome = req.Nome ?? cargo.Nome;
        cargo.Descricao = req.Descricao ?? cargo.Descricao;
        
        var permissoes = await _context.Permissoes.Where(p => req.Permissoes.Contains(p.Nome)).ToListAsync(ct);
        cargo.Permissoes = permissoes.Select(p => new PermissaoCargo
        {
            Permissao = p,
            Cargo = cargo
        }).ToList();
        
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(cargo.ToDto(), ct);
    }
    
    
    
}