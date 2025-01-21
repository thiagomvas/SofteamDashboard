using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class Projeto
{
    public int Id { get; set; } 
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public StatusProjeto Status { get; set; } = StatusProjeto.Lead;
    public DateTime Inicio { get; set; } = DateTime.Now;
    public DateTime Fim { get; set; } = DateTime.Now;
    public ICollection<Funcionario> Funcionarios { get; set; }
}