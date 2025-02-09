namespace SofteamDashboard.Server.Models;

public class CreateProjetoRequest
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }

}