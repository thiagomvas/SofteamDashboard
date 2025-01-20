using SofteamDashboard.Core.Entities;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Abstractions;

public interface IFuncionarioService
{
    Task<IEnumerable<Funcionario>> GetComNivelDeHabilidadeMinimaAsync(Habilidade habilidade, Nivel nivel, bool somenteDisponivel = false, CancellationToken ct = default);
    Task<IEnumerable<Funcionario>> GetDisponivelAsync(CancellationToken ct = default);
}