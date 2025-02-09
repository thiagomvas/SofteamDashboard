namespace SofteamDashboard.Server.Models.Requests;

public class GetCargosRequest
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 100;
    public int? Id { get; set; }
    public bool IncludeFuncionarios { get; set; } = false;
}