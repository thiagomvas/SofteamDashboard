using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Cargos;

public class CreateCargo : Endpoint<CreateCargoRequest, CargoDTO>
{
    private readonly SofteamDbContext _context;
    
    public CreateCargo(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/api/cargos");
        Permissions(Constants.ADMIN);
        Summary(s =>
        {
            s.Summary = "Cria um novo cargo.";
            s.Description = "Cria um novo cargo com as informações fornecidas.";
            s.Responses[200] = "Cargo criado com sucesso.";
            s.Responses[400] = "Erro ao criar cargo.";
        });
    }
    
    public override async Task HandleAsync(CreateCargoRequest req, CancellationToken ct)
    {
        var cargo = new Cargo
        {
            Nome = req.Nome,
            Descricao = req.Descricao,
        };
        
        var permissoes = await _context.Permissoes.Where(p => req.Permissoes.Contains(p.Nome)).ToListAsync(ct);
        cargo.Permissoes = permissoes.Select(p => new PermissaoCargo
        {
            Permissao = p,
            Cargo = cargo
        }).ToList();
        
        await _context.Cargos.AddAsync(cargo, ct);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(cargo.ToDto(), ct);
    }
    
}