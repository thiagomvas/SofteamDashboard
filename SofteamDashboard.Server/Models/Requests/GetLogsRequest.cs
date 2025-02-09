using FastEndpoints;

namespace SofteamDashboard.Server.Models.Requests;

public class GetLogsRequest
{
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    public int? UserId { get; set; }
}