using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.Responses;
using SofteamDashboard.Server.Services;

namespace SofteamDashboard.Server.Endpoints.Auth;

public class Register : Endpoint<RegisterRequest, AuthResponse>
{
    private readonly SofteamDbContext _context;
    private readonly AuthService _authService;
    
    public Register(SofteamDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public override void Configure()
    {
        Post("api/auth/register");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(RegisterRequest request, CancellationToken ct)
    {
        var creds = new Credenciais()
        {
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        
        var funcionario = new Funcionario()
        {
            Nome = request.Username
        };
        _context.Funcionarios.Add(funcionario);
        creds.Funcionario = funcionario;
        
        _context.Credenciais.Add(creds);
        await _context.SaveChangesAsync(ct);

        creds = await _context.Credenciais.Include(c => c.Funcionario)
            .FirstOrDefaultAsync(c => c.Username == creds.Username);
        
        var response = new AuthResponse()
        {
            Token = _authService.GenerateJwtToken(creds),
            Id = creds.Funcionario.Id
        };
        
        await SendOkAsync(response, ct);
    }
}