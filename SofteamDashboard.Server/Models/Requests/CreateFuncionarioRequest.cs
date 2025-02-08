namespace SofteamDashboard.Server.Models;

public class CreateFuncionarioRequest
{
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public string? Linkedin { get; set; } = null;
    public string? Github { get; set; } = null;
    public int? CargoId { get; set; } = null;
}