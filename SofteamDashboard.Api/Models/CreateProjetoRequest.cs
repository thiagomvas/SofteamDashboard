namespace SofteamDashboard.Api.Models;

public class CreateProjetoRequest
{
    public string Titulo { get; set; } = "";
    public string Descricao { get; set; } = "";
    public DateTime Inicio { get; set; } = DateTime.Now;
    public DateTime Fim { get; set; } = DateTime.Now;
    public int ResponsavelId { get; set; } 
    public string GithubUrl { get; set; } = "";
}