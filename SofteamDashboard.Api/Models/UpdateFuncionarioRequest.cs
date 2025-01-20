using SofteamDashboard.Core;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Api.Models;

public class UpdateFuncionarioRequest
{
    public string? Nome { get; set; }
    public string? GithubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public Cargo? Cargo { get; set; }
    public Area? Area { get; set; }
}