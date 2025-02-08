using FastEndpoints;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Funcionarios;

public class CreateFuncionario : Endpoint<CreateFuncionarioRequest, FuncionarioDTO>
{
    private readonly SofteamDbContext _context;
    
    public CreateFuncionario(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/api/funcionarios");
        Permissions(Constants.ADMIN, Constants.MANAGE_FUNCIONARIOS);
    }

    public override async Task HandleAsync(CreateFuncionarioRequest req, CancellationToken ct)
    {
        var funcionario = new Funcionario
        {
            Nome = req.Nome,
            Email = req.Email,
            Linkedin = req.Linkedin,
            Github = req.Github,
            CargoId = req.CargoId
        };
        
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(funcionario.ToDto(), ct);
    }
}