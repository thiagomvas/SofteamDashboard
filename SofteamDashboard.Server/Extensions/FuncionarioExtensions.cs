using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Models.DTOs;

namespace SofteamDashboard.Server.Extensions;

public static class FuncionarioExtensions
{
    public static FuncionarioDTO ToDto(this Funcionario funcionario)
    {
        return new FuncionarioDTO
        {
            Id = funcionario.Id,
            Nome = funcionario.Nome,
            Email = funcionario.Email,
            Linkedin = funcionario.Linkedin,
            Github = funcionario.Github,
            CargoId = funcionario.CargoId,
            Cargo = funcionario.Cargo?.Nome,
            CreatedAt = funcionario.CreatedAt
        };
    }
}