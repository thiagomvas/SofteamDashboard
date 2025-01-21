using SofteamDashboard.Core;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class FuncionarioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public Cargo Cargo { get; set; } = Cargo.Membro;
    public Area Area { get; set; } = Area.Nenhum;

    public ICollection<HabilidadeFuncionarioDTO> Habilidades { get; set; }
}