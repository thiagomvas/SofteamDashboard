using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Server.Models.DTOs;

public class ProjetoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Status Status { get; set; }
    
    public List<FuncionarioDTO> Membros { get; set; } = [];
}