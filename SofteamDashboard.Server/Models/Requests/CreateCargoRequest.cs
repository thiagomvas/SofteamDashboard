namespace SofteamDashboard.Server.Models;

public class CreateCargoRequest
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public string[] Permissoes { get; set; }
}