using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class HabilidadeFuncionario
{
    public int Id { get; set; }
    
    public int FuncionarioId { get; set; }
    public string NomeHabilidade { get; set; }
    public Nivel Nivel { get; set; } 
    public bool Verificado { get; set; } = false;
    
    public Funcionario Funcionario { get; set; }

}