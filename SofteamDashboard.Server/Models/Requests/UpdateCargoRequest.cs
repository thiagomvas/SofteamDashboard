namespace SofteamDashboard.Server.Endpoints.Cargos;

public class UpdateCargoRequest
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string[]? Permissoes { get; set; }
}