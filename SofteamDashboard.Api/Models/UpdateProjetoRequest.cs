namespace SofteamDashboard.Api.Models;

public class UpdateProjetoRequest
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime? Inicio { get; set; }
    public DateTime? Fim { get; set; }
    public string? GithubUrl { get; set; }
    public int? ResponsavelId { get; set; }
}