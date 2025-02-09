namespace SofteamDashboard.Server.Models.Requests;

public class GetFuncionariosRequest
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = 100;
    public bool IncludeCargo { get; set; } = false;
    public bool IncludePermissoes { get; set; } = false;
    public int? Id { get; set; }
    
}