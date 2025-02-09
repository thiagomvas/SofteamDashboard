using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class MembroProjeto : BaseEntity
{
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
    public int ProjetoId { get; set; }
    public Projeto Projeto { get; set; }
    public CargoProjeto Cargo { get; set; }
}