using SofteamDashboard.Api.Models;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Api.Extensions;

public static class FuncionarioExtensions
{
    public static HabilidadeFuncionarioDTO ToDto(this HabilidadeFuncionario habilidade)
    {
        return new HabilidadeFuncionarioDTO()
        {
            Id = habilidade.Id,
            Nivel = habilidade.Nivel,
            NomeHabilidade = habilidade.NomeHabilidade,
            Verificado = habilidade.Verificado
        };
    }
    
    public static FuncionarioDTO ToDto(this Funcionario funcionario)
    {
        var result = new FuncionarioDTO()
        {
            Area = funcionario.Area,
            Cargo = funcionario.Cargo,
            GithubUrl = funcionario.GithubUrl,
            LinkedInUrl = funcionario.LinkedInUrl,
            Nome = funcionario.Nome
        };
        
        if(funcionario.Habilidades is not null)
            result.Habilidades = funcionario.Habilidades.Select(h => h.ToDto()).ToList();

        return result;
    }
}