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
    
    public static FuncionarioDTO ToDto(this Funcionario funcionario, bool callChild = true)
    {
        var result = new FuncionarioDTO()
        {
            Id = funcionario.Id,
            Area = funcionario.Area,
            Cargo = funcionario.Cargo,
            GithubUrl = funcionario.GithubUrl,
            LinkedInUrl = funcionario.LinkedInUrl,
            Nome = funcionario.Nome,
            ProjetoId = funcionario.ProjetoId ?? 0,
        };
        
        
        if (funcionario.Projeto is not null && callChild)
            result.Projeto = funcionario.Projeto.ToDto(false);
        
        
        if(funcionario.Habilidades is not null)
            result.Habilidades = funcionario.Habilidades.Select(h => h.ToDto()).ToList();

        return result;
    }

    public static ProjetoDTO ToDto(this Projeto projeto, bool callChild = true)
    {
        var result =  new ProjetoDTO()
        {
            Id = projeto.Id,
            Titulo = projeto.Titulo,
            Descricao = projeto.Descricao,
            Inicio = projeto.Inicio,
            Fim = projeto.Fim,
            GithubUrl = projeto.GithubUrl,
            ResponsavelId = projeto.ResponsavelId ?? 0,
            Status = projeto.Status
        };
        if (projeto.Responsavel is not null && callChild)
            result.Responsavel = projeto.Responsavel.ToDto(false);

        return result;
    }
}