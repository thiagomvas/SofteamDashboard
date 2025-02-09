using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class UpdateFuncionarioInProjeto : Endpoint<UpdateFuncionarioInProjetoRequest, ProjetoDTO>
{
    private readonly SofteamDbContext _context;
    
    public UpdateFuncionarioInProjeto(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/api/projetos/{projetoId}/funcionarios/{funcionarioId}");
        Permissions(Constants.ADMIN, Constants.MANAGE_PROJETOS);
        Summary(s =>
        {
            s.Summary = "Atualiza um funcionário em um projeto.";
            s.Description = "Atualiza um funcionário em um projeto com o ID fornecido.";
            s.Responses[200] = "Funcionário atualizado no projeto com sucesso.";
            s.Responses[404] = "Projeto ou funcionário não encontrado.";
        });
    }
    
    
    public override async Task HandleAsync(UpdateFuncionarioInProjetoRequest req, CancellationToken ct)
    {
        int projetoId = Route<int>("projetoId");
        int funcionarioId = Route<int>("funcionarioId");
        
        var projeto = await _context.Projetos.Include(p => p.Membros).FirstOrDefaultAsync(p => p.Id == projetoId, ct);
        if (projeto == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var funcionario = await _context.Funcionarios.FindAsync(funcionarioId, ct);
        if (funcionario == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var membroProjeto = projeto.Membros.FirstOrDefault(m => m.FuncionarioId == funcionarioId);
        
        if (membroProjeto == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        membroProjeto.Cargo = req.Cargo ?? membroProjeto.Cargo;
        
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(projeto.ToDto(), ct);
    }

}