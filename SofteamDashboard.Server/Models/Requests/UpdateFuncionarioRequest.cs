namespace SofteamDashboard.Server.Models;

public class UpdateFuncionarioRequest
{
    
    public string? Nome { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Linkedin { get; set; } = null;
    public string? Github { get; set; } = null;
    public int? CargoId { get; set; } = null;
}