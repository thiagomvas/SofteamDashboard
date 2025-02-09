using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Funcionarios;

public class GetAllFuncionarios : EndpointWithoutRequest<IEnumerable<FuncionarioDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetAllFuncionarios(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/funcionarios");
        AllowAnonymous();
        Summary(s => 
        {
            s.Summary = "Retorna todos os funcionários cadastrados.";
            s.Description = "Retorna todos os funcionários cadastrados com varios niveis de aprofundamento.\n" +
                "Se o parametro 'id' for informado, retorna uma array contendo somente o funcionário com o id informado.\n" +
                "Se o parametro 'includeCargo' (bool) for informado, inclui o cargo do funcionário.\n" +
                "Se o parametro 'includePermissoes' (bool) for informado, inclui as permissões do cargo do funcionário.";
            s.Params = new Dictionary<string, string>()
            {
                { "id", "Id do funcionário a ser retornado." },
                { "includeCargo", "Inclui o cargo do funcionário." },
                { "includePermissoes", "Inclui as permissões do cargo do funcionário." }
            };
            s.Responses[200] = "Funcionários retornados com sucesso.";
            s.Responses[404] = "Funcionário não encontrado.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Query<int?>("id", isRequired: false);
        var includeCargo = Query<bool>("includeCargo", isRequired: false);
        var includePermissoes = Query<bool>("includePermissoes", isRequired: false);

        IQueryable<Funcionario> query = _context.Funcionarios;

        if (includeCargo)
        {
            if (includePermissoes)
            {
                query = query.Include(f => f.Cargo).ThenInclude(c => c.Permissoes)
                    .ThenInclude(cp => cp.Permissao);
            }
            else
            {
                query = query.Include(f => f.Cargo);
            }
        }

        if (id.HasValue)
        {
            var funcionario = await query.FirstOrDefaultAsync(f => f.Id == id.Value, ct);
            if (funcionario is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }
            await SendOkAsync([funcionario.ToDto()], ct);
        }
        else
        {
            var funcionarios = await query.ToListAsync(ct);
            await SendOkAsync(funcionarios.Select(f => f.ToDto()), ct);
        }
    }

}