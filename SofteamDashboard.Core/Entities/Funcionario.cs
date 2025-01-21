using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public Cargo Cargo { get; set; } = Cargo.Membro;
    public Area Area { get; set; } = Area.Nenhum;

    public int ProjetoId { get; set; } = 1;
    public Projeto Projeto { get; set; }

    public ICollection<HabilidadeFuncionario> Habilidades { get; set; }
}