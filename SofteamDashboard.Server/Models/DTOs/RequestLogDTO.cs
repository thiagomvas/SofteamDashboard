namespace SofteamDashboard.Server.Models.DTOs;

public class RequestLogDTO
{
    public int Id { get; set; }
    public string? User { get; set; }
    public string Path { get; set; }
    public string Method { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Body { get; set; }
    
}