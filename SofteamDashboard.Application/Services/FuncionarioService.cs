using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Abstractions;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly SofteamDbContext _context;

    public FuncionarioService(SofteamDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Funcionario>> GetComNivelDeHabilidadeMinimaAsync(Habilidade habilidade, Nivel nivel, bool somenteDisponivel = false,
        CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Funcionario>> GetDisponivelAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}