using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class Cargo : BaseEntity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    
    public ICollection<PermissaoCargo> Permissoes { get; set; }
    public ICollection<Funcionario> Funcionarios { get; set; }
}