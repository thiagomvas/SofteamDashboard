using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class Projeto
{
    public int Id { get; set; } 
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public StatusProjeto Status { get; set; } = StatusProjeto.Lead;
    public string GithubUrl { get; set; } = string.Empty;
    public DateTime Inicio { get; set; } = DateTime.Now;
    public DateTime Fim { get; set; } = DateTime.Now;
    
    public int? ResponsavelId { get; set; }
    public Funcionario? Responsavel { get; set; }
    public ICollection<Funcionario> Funcionarios { get; set; }
}