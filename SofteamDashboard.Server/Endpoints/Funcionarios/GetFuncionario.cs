using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Funcionarios;

public class GetFuncionario : EndpointWithoutRequest<FuncionarioDTO>
{
    private readonly SofteamDbContext _context;
    
    public GetFuncionario(SofteamDbContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Get("/api/funcionarios/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var idParam = Route<int>("id");
        var funcionario = await _context.Funcionarios
            .Include(f => f.Cargo)
            .FirstOrDefaultAsync(f => f.Id == idParam, ct);

        if (funcionario is null)
            await SendNotFoundAsync(ct);
        
        ThrowIfAnyErrors();
        
        await SendOkAsync(funcionario!.ToDto(), ct);
    }

}