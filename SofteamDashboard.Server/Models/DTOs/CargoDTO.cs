using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Server.Models.DTOs;

public class CargoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Status Status { get; set; }

    public List<PermissaoDTO> Permissoes { get; set; } = [];
    public List<FuncionarioDTO> Funcionarios { get; set; } = [];

}