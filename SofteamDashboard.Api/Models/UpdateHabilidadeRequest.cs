using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class UpdateHabilidadeRequest
{
    public string? Nome { get; set; }
    public Nivel? Nivel { get; set; }
}