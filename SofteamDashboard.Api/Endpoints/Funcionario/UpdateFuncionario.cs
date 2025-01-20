using FastEndpoints;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Api.Extensions;
using SofteamDashboard.Api.Models;
using SofteamDashboard.Application;

namespace SofteamDashboard.Api.Endpoints.Funcionario;

public class UpdateFuncionario : Endpoint<UpdateFuncionarioRequest, FuncionarioDTO>
{
    private readonly SofteamDbContext _context;

    public UpdateFuncionario(SofteamDbContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Put("api/funcionarios/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateFuncionarioRequest req, CancellationToken ct)
    {
        var id = Route<int>("id");

        var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id, cancellationToken: ct);

        if (funcionario is null)
        {
            ValidationFailures.Add(new ValidationFailure("id", "Funcionario with specified id not found"));
            await SendErrorsAsync(StatusCodes.Status404NotFound, ct);
        }
        
        ThrowIfAnyErrors();

        funcionario!.Nome = req.Nome ?? funcionario.Nome;
        funcionario.GithubUrl = req.GithubUrl ?? funcionario.GithubUrl;
        funcionario.LinkedInUrl = req.LinkedInUrl ?? funcionario.LinkedInUrl;
        funcionario.Cargo = req.Cargo ?? funcionario.Cargo;
        funcionario.Area = req.Area ?? funcionario.Area;

        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync(ct);

        var dto = funcionario.ToDto();
        await SendCreatedAtAsync($"api/funcionarios/{funcionario.Id}", dto, dto, cancellation: ct );
    }
}