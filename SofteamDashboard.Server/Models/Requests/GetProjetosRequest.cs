namespace SofteamDashboard.Server.Models.Requests;

public class GetProjetosRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool IncludeFuncionarios { get; set; }
}