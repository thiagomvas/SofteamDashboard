using System.Net;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Models;
using SofteamDashboard.Server.Models.Responses;
using SofteamDashboard.Server.Services;

namespace SofteamDashboard.Server.Endpoints.Auth;

public class Login : Endpoint<LoginRequest, AuthResponse>
{
    private readonly AuthService _authService;
    private readonly SofteamDbContext _context;
    
    public Login(AuthService authService, SofteamDbContext context)
    {
        _authService = authService;
        _context = context;
    }
    
    public override void Configure()
    {
        Post("api/auth/login");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Autentica um usuário.";
            s.Description = "Autentica um usuário com as credenciais informadas e retorna um token JWT.";
            s.Responses[200] = "Usuário autenticado com sucesso.";
            s.Responses[403] = "Credenciais inválidas.";
        });
    }
    
    public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
    {
        var creds = await _context.Credenciais.Include(c => c.Funcionario)
            .FirstOrDefaultAsync(c => c.Username == request.Username);
        
        if (creds == null || !BCrypt.Net.BCrypt.Verify(request.Password, creds.PasswordHash))
        {
            await SendForbiddenAsync(ct);
            return;
        }
        
        var response = new AuthResponse()
        {
            Token = await _authService.GenerateJwtToken(creds),
            Id = creds.Funcionario.Id
        };
        
        await SendOkAsync(response, ct);
    }
    
}