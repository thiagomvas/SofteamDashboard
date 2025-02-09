using System.Security.Claims;
using System.Text;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Server.Services;

public class AuthService
{
    private readonly SofteamDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(SofteamDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    public async Task<string> GenerateJwtToken(Credenciais credenciais)
    {
        var permissions = await _context.PermissaoCargos
            .Where(p => p.CargoId == credenciais.Funcionario.CargoId)
            .Select(p => p.Permissao.Nome)
            .ToListAsync();
        return JwtBearer.CreateToken(o =>
        {
            o.SigningKey = _config["Jwt:Key"];
            o.Issuer = _config["Jwt:Issuer"];
            o.Audience = _config["Jwt:Audience"];
            o.User.Claims.Add((Constants.NAME, credenciais.Username));
            o.User.Permissions.AddRange(permissions);
        });
    }
}