using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;

namespace SofteamDashboard.Server.Endpoints.Funcionarios;

public class DeleteFuncionario : EndpointWithoutRequest
{
    private readonly SofteamDbContext _context;
    
    public DeleteFuncionario(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/funcionarios/{id}");
        Permissions(Constants.ADMIN, Constants.MANAGE_FUNCIONARIOS);
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        
        var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id, ct);
        if (funcionario == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        _context.Funcionarios.Remove(funcionario);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}