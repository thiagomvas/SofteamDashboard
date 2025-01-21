using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Web.Models;

public class HabilidadeDTO
{
    public int Id { get; set; }
    public string NomeHabilidade { get; set; }
    public Nivel Nivel { get; set; } 
    public bool Verificado { get; set; } = false;
}