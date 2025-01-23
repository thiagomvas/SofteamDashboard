using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Web.Models;

public class ProjetoDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime? Inicio { get; set; } = DateTime.Now;
    public DateTime? Fim { get; set; } = DateTime.Now;
    public StatusProjeto Status { get; set; }
    public string GithubUrl { get; set; } = string.Empty;
    public int ResponsavelId { get; set; }
    public FuncionarioDTO? Responsavel { get; set; }

    public ICollection<FuncionarioDTO> Funcionarios { get; set; } = [];
}