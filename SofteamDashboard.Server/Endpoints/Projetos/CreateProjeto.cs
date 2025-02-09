using FastEndpoints;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class CreateProjeto : Endpoint<CreateProjetoRequest, ProjetoDTO>
{
    private readonly SofteamDbContext _context;
    
    public CreateProjeto(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/api/projetos");
        Permissions(Constants.ADMIN, Constants.MANAGE_PROJETOS);
        Summary(s =>
        {
            s.Summary = "Cria um novo projeto.";
            s.Description = "Cria um novo projeto com as informações fornecidas.";
            s.Responses[200] = "Projeto criado com sucesso.";
            s.Responses[400] = "Erro ao criar projeto.";
        });
    }
    
    public override async Task HandleAsync(CreateProjetoRequest req, CancellationToken ct)
    {
        var projeto = new Projeto
        {
            Nome = req.Nome,
            Descricao = req.Descricao,
            DataInicio = req.DataInicio,
            DataFim = req.DataFim
        };
        
        _context.Projetos.Add(projeto);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(projeto.ToDto(), ct);
    }
}