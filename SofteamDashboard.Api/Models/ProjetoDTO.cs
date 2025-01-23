using SofteamDashboard.Core.Entities;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class ProjetoDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public StatusProjeto Status { get; set; }
    public string GithubUrl { get; set; }
    public int ResponsavelId { get; set; }
    public FuncionarioDTO Responsavel { get; set; }
    
    public ICollection<FuncionarioDTO> Funcionarios { get; set; }
}