using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class Funcionario : BaseEntity
{
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public string? Linkedin { get; set; } = null;
    public string? Github { get; set; } = null;
    
    public int? CargoId { get; set; } = null;
    public Cargo? Cargo { get; set; } = null;
    
    public int? CredenciaisId { get; set; }
    public Credenciais? Credenciais { get; set; } = null!;
    
    public ICollection<MembroProjeto> Alocacoes { get; set; }
}