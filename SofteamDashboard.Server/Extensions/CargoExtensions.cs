using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Extensions;

public static class CargoExtensions
{
    
    public static CargoDTO ToDto(this Cargo cargo)
    {
        var result = new CargoDTO
        {
            Id = cargo.Id,
            Nome = cargo.Nome,
            Descricao = cargo.Descricao,
            CreatedAt = cargo.CreatedAt,
            UpdatedAt = cargo.UpdatedAt,
            Status = cargo.Status,
        };
        if(cargo.Permissoes is not null)
            result.Permissoes = cargo.Permissoes.Select(p => p.Permissao.ToDto()).ToList();
        if(cargo.Funcionarios is not null)
            result.Funcionarios = cargo.Funcionarios.Select(f => f.ToDto()).ToList();
        
        return result;
    }
    
}