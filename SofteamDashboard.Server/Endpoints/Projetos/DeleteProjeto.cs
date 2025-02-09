using FastEndpoints;
using SofteamDashboard.Core;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class DeleteProjeto : EndpointWithoutRequest
{
    private readonly SofteamDbContext _context;
    
    public DeleteProjeto(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/projetos/{id}");
        Permissions(Constants.ADMIN, Constants.MANAGE_PROJETOS);
        Summary(s =>
        {
            s.Summary = "Deleta um projeto.";
            s.Description = "Deleta um projeto com o ID fornecido.";
            s.Responses[204] = "Projeto deletado com sucesso.";
            s.Responses[404] = "Projeto não encontrado.";
        });
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        int id = Route<int>("id");
        
        var projeto = await _context.Projetos.FindAsync([id], ct);
        
        if (projeto is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        _context.Projetos.Remove(projeto);
        
        await _context.SaveChangesAsync(ct);
        
        await SendNoContentAsync(ct);
    }
    
}