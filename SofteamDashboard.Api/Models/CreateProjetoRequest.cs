namespace SofteamDashboard.Api.Models;

public class CreateProjetoRequest
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
}