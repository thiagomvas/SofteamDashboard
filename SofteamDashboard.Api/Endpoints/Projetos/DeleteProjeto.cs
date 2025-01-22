using FastEndpoints;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Projetos;

public class DeleteProjeto : EndpointWithoutRequest
{
    private readonly SofteamDbContext _context;
    
    public DeleteProjeto(SofteamDbContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Delete("api/projetos/{id}");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var projeto = await _context.Projetos.FindAsync(id);
        
        if(projeto is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        _context.Projetos.Remove(projeto);
        await _context.SaveChangesAsync(ct);
        
        await SendNoContentAsync(ct);
    }
}