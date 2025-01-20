using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class CreateHabilidadeRequest
{
    public string Nome { get; set; }
    public Nivel Nivel { get; set; } = Nivel.Nenhum;
}