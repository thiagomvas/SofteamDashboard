using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Funcionario;

public class DeleteFuncionario : EndpointWithoutRequest
{
    private readonly SofteamDbContext _context;

    public DeleteFuncionario(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("api/funcionarios/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id, ct);

        if (funcionario is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            await SendNoContentAsync(ct);
        }
        
        
    }
}