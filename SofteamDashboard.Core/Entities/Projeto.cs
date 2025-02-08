using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class Projeto : BaseEntity
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }

    
    public ICollection<MembroProjeto> Membros { get; set; }
}