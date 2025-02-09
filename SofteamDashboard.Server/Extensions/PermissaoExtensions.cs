using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Extensions;

public static class PermissaoExtensions
{
        
        public static PermissaoDTO? ToDto(this Permissao? permissao)
        {
            if (permissao is null)
                return null;
            return new PermissaoDTO
            {
                Id = permissao.Id,
                Nome = permissao.Nome,
                Descricao = permissao.Descricao,
                CreatedAt = permissao.CreatedAt,
                UpdatedAt = permissao.UpdatedAt,
                Status = permissao.Status
            };
        }
    
}