using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Extensions;

public static class ProjetoExtensions
{
    public static ProjetoDTO ToDto(this Projeto projeto)
    {
        var result = new ProjetoDTO
        {
            Id = projeto.Id,
            Nome = projeto.Nome,
            Descricao = projeto.Descricao,
            DataInicio = projeto.DataInicio,
            DataFim = projeto.DataFim,
            CreatedAt = projeto.CreatedAt,
            UpdatedAt = projeto.UpdatedAt,
            Status = projeto.Status,
        };
        
        if (projeto.Membros != null)
        {
            result.Membros = projeto.Membros.Select(m => m.Funcionario.ToDto()).ToList();
        }
        
        return result;
    }
    
}