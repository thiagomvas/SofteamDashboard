using SofteamDashboard.Core;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Web.Models;

public class FuncionarioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public Cargo Cargo { get; set; } = Cargo.Membro;
    public Area Area { get; set; } = Area.Nenhum;

    public bool Collapsed { get; set; } = true;
    
    public int ProjetoId { get; set; }

    public ICollection<HabilidadeDTO> Habilidades { get; set; } = [];
}