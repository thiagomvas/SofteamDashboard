namespace SofteamDashboard.Web.Models;

public class ProjetoDTO
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public string GithubUrl { get; set; }
    public int ResponsavelId { get; set; }
    
    public ICollection<FuncionarioDTO> Funcionarios { get; set; }
}