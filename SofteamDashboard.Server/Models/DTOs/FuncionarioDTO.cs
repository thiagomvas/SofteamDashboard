namespace SofteamDashboard.Server.Models.DTOs;

public class FuncionarioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public string? Linkedin { get; set; } = null;
    public string? Github { get; set; } = null;
    public int? CargoId { get; set; } = null;
    public string? Cargo { get; set; } = null;
    public DateTime CreatedAt { get; set; }
}