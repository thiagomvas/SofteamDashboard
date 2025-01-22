using FastEndpoints;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Projetos;

public class UpdateProjeto : Endpoint<UpdateProjetoRequest, ProjetoDTO>
{
    private readonly SofteamDbContext _context;
    
    public UpdateProjeto(SofteamDbContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Put("api/projetos/{id}");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(UpdateProjetoRequest request, CancellationToken ct)
    {
        var id = Route<int>("id");
        var projeto = await _context.Projetos.FindAsync(id);
        
        if(projeto is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        projeto.Titulo = request.Titulo ?? projeto.Titulo;
        projeto.Descricao = request.Descricao ?? projeto.Descricao;
        projeto.Inicio = request.Inicio ?? projeto.Inicio;
        projeto.Fim = request.Fim ?? projeto.Fim;
        
        await _context.SaveChangesAsync(ct);
        
        await SendOkAsync(projeto.ToDto(), ct);
    }
    
}