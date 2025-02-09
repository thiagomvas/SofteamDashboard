using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Projetos;

public class DeleteFuncionarioFromProjeto : EndpointWithoutRequest<ProjetoDTO>
{
    private readonly SofteamDbContext _context;

    public DeleteFuncionarioFromProjeto(SofteamDbContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Delete("/api/projetos/{projetoId}/funcionarios/{funcionarioId}");
        Permissions(Constants.ADMIN, Constants.MANAGE_PROJETOS);
        Summary(s =>
        {
            s.Summary = "Remove um funcionário de um projeto.";
            s.Description = "Remove um funcionário do projeto com o ID fornecido.";
            s.Responses[200] = "Funcionário removido do projeto com sucesso.";
            s.Responses[404] = "Projeto ou funcionário não encontrado.";
        });
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var projeto = await _context.Projetos
            .Include(p => p.Membros)
            .FirstOrDefaultAsync(p => p.Id == 1, cancellationToken);

        if (projeto is null)
        {
            await SendNotFoundAsync();
            return;
        }

        var funcionario = projeto.Membros.FirstOrDefault(f => f.Id == 1);

        if (funcionario is null)
        {
            await SendNotFoundAsync();
            return;
        }

        projeto.Membros.Remove(funcionario);

        await _context.SaveChangesAsync(cancellationToken);

        await SendOkAsync(projeto.ToDto());
    }
    
}