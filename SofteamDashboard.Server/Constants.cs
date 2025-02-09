namespace SofteamDashboard.Server;

public static class Constants
{
    #region Claims
    public const string NAME = "Name";
    public const string USERID = "UserId";
    #endregion
    
    #region Permissoes
    public const string ADMIN = "ADMIN";
    public const string MANAGE_FUNCIONARIOS = "MANAGE_FUNCIONARIOS";
    public const string MANAGE_CARGOS = "MANAGE_CARGOS";
    public const string MANAGE_PROJETOS = "MANAGE_PROJETOS";
    public const string VIEW_METRICAS = "VIEW_METRICAS";
    public const string VIEW_FUNCIONARIOS = "VIEW_FUNCIONARIOS";
    public const string VIEW_CARGOS = "VIEW_CARGOS";
    public const string VIEW_PROJETOS = "VIEW_PROJETOS";
    #endregion

    #region Cargos
    public const string DIRETOR = "Diretor";
    public const string DEV = "Desenvolvedor";
    public const string RH = "Recursos Humanos";
    public const string FINANCEIRO = "Financeiro";
    public const string MARKETING = "Marketing";
    #endregion
}