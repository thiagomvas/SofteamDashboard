using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Server.Models;

public class UpdateProjetoRequest
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public Status? Status { get; set; }
    
}