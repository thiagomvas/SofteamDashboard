using FastEndpoints;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Api.Endpoints.Funcionario;

public class CreateFuncionario : Endpoint<CreateFuncionarioRequest, FuncionarioDTO>
{
    private readonly SofteamDbContext _context;
    
    public CreateFuncionario(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Post("api/funcionarios");
        AllowAnonymous();
    }

    public override Task HandleAsync(CreateFuncionarioRequest req, CancellationToken ct)
    {
        var funcionario = new Core.Entities.Funcionario()
        {
            Nome = req.Nome,
            GithubUrl = req.GithubUrl,
            LinkedInUrl = req.LinkedInUrl,
            Cargo = req.Cargo,
            Area = req.Area,
            Habilidades = req.Habilidades.Select(h => new HabilidadeFuncionario()
            {
                NomeHabilidade = h.Nome,
                Nivel = h.Nivel
            }).ToList()
        };

        _context.Funcionarios.Add(funcionario);
        _context.SaveChanges();
        
        return SendOkAsync(funcionario.ToDto(), ct);
    }
}