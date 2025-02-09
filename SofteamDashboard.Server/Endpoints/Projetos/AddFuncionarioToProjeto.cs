using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class AddFuncionarioToProjeto : EndpointWithoutRequest<ProjetoDTO>
{
    private readonly SofteamDbContext _context;
    
    public AddFuncionarioToProjeto(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Post("/api/projetos/{id}/funcionarios/{funcionarioId}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");
        int funcionarioId = Route<int>("funcionarioId");

        var projeto = await _context.Projetos
            .Include(p => p.Membros)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

        if (projeto == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var funcionario = await _context.Funcionarios
            .FirstOrDefaultAsync(f => f.Id == funcionarioId, ct);

        if (funcionario == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var membroProjeto = new MembroProjeto
        {
            ProjetoId = projeto.Id,
            FuncionarioId = funcionario.Id
        };
        
        projeto.Membros.Add(membroProjeto);
        
        await _context.SaveChangesAsync(ct);
        
        await SendOkAsync(projeto.ToDto(), ct);
    }
    
}