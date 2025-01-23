using FastEndpoints;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Projetos;

public class GetProjeto : EndpointWithoutRequest<ProjetoDTO>
{
    private readonly SofteamDbContext _context;
    
    public GetProjeto(SofteamDbContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Get("api/projetos/{id}");
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
        
        await SendOkAsync(projeto.ToDto(), ct);
    }
}