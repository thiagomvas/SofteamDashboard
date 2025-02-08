namespace SofteamDashboard.Core.Entities;

public class Credenciais : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
}