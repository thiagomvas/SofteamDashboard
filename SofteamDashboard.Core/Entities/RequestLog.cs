namespace SofteamDashboard.Core.Entities;

public class RequestLog
{
    public int Id { get; set; }
    public string? User { get; set; }
    public string Path { get; set; }
    public string Method { get; set; }
    public DateTime Timestamp { get; set; }
}