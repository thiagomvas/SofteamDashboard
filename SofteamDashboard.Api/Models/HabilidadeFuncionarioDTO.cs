using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class HabilidadeFuncionarioDTO
{
    public int Id { get; set; }
    public string NomeHabilidade { get; set; }
    public Nivel Nivel { get; set; } 
    public bool Verificado { get; set; } = false;
}