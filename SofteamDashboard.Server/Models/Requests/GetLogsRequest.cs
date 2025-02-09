using FastEndpoints;

namespace SofteamDashboard.Server.Models.Requests;

public class GetLogsRequest
{
    public int Page { get; set; } = 0;

    public int PageSize { get; set; } = 100;
    
    public int? UserId { get; set; }
}