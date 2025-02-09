using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class UpdateProjeto : Endpoint<UpdateProjetoRequest, ProjetoDTO>
{
    private readonly SofteamDbContext _context;
    
    public UpdateProjeto(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/api/projetos/{id}");
        Permissions(Constants.ADMIN, Constants.MANAGE_PROJETOS);
        Summary(s =>
        {
            s.Summary = "Atualiza um projeto.";
            s.Description = "Atualiza um projeto com o ID fornecido.";
            s.Responses[200] = "Projeto atualizado com sucesso.";
            s.Responses[404] = "Projeto não encontrado.";
        });
    }
    
    
    public override async Task HandleAsync(UpdateProjetoRequest req, CancellationToken ct)
    {
        int id = Route<int>("id");
        var projeto = await _context.Projetos.FindAsync([id], ct);
        if (projeto == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        projeto.Nome = req.Nome ?? projeto.Nome;
        projeto.Descricao = req.Descricao ?? projeto.Descricao;
        projeto.DataInicio = req.DataInicio ?? projeto.DataInicio;
        projeto.DataFim = req.DataFim ?? projeto.DataFim;
        projeto.Status = req.Status ?? projeto.Status;
        
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(projeto.ToDto(), ct);
    }
    
}