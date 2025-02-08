using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Extensions;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Endpoints.Funcionarios;

public class UpdateFuncionario : Endpoint<UpdateFuncionarioRequest, FuncionarioDTO>
{
    private readonly SofteamDbContext _context;
    
    public UpdateFuncionario(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/api/funcionarios/{id}");
        Permissions(Constants.ADMIN, Constants.MANAGE_FUNCIONARIOS);
    }

    public override async Task HandleAsync(UpdateFuncionarioRequest req, CancellationToken ct)
    {
        var id = Route<int>("id");
        
        var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id, ct);
        if (funcionario == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        funcionario.Nome = req.Nome ?? funcionario.Nome;
        funcionario.Email = req.Email ?? funcionario.Email;
        funcionario.Linkedin = req.Linkedin ?? funcionario.Linkedin;
        funcionario.Github = req.Github ?? funcionario.Github;
        funcionario.CargoId = req.CargoId ?? funcionario.CargoId;
        
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(funcionario.ToDto(), ct);
    }
}