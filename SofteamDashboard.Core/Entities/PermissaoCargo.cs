namespace SofteamDashboard.Core.Entities;

public class PermissaoCargo : BaseEntity
{
    public int CargoId { get; set; }
    public Cargo Cargo { get; set; }
    
    public int PermissaoId { get; set; }
    public Permissao Permissao { get; set; }
}