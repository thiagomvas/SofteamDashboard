using System.Text;
using FastEndpoints.Security;
using Microsoft.IdentityModel.Tokens;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Server;

public class AuthService
{
    private readonly SofteamDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(SofteamDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public string GenerateJwtToken(Credenciais credenciais)
    {
        return JwtBearer.CreateToken(o =>
        {
            o.SigningKey = _config["Jwt:Key"];
            o.Issuer = _config["Jwt:Issuer"];
            o.Audience = _config["Jwt:Audience"];
        });
    }
}