using FastEndpoints;
using SofteamDashboard.Core;

namespace SofteamDashboard.Server.Endpoints.Cargos;

public class DeleteCargo : EndpointWithoutRequest
{
    private readonly SofteamDbContext _context;

    public DeleteCargo(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/cargos/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var cargo = await _context.Cargos.FindAsync([id], ct);

        if (cargo is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Cargos.Remove(cargo);
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
    
}