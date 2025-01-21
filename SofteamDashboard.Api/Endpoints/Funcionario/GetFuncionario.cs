using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Funcionario;

public class GetFuncionario : EndpointWithoutRequest<FuncionarioDTO>
{
    private readonly SofteamDbContext _context;

    public GetFuncionario(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("api/funcionarios/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var query = _context.Funcionarios
            .Include(f => f.Habilidades)
            .Include(f => f.Projeto);


        var result = await query.FirstOrDefaultAsync(f => f.Id == id, ct);

        if (result is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(result.ToDto(), ct);
    }
}