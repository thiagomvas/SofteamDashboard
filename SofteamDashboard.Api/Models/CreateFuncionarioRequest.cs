using SofteamDashboard.Core;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class CreateFuncionarioRequest
{
    public string Nome { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public Cargo Cargo { get; set; } = Cargo.Membro;
    public Area Area { get; set; } = Area.Nenhum;

    public IEnumerable<CreateHabilidadeRequest> Habilidades { get; set; } = [];
}