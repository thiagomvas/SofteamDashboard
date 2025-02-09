using System.Security.Claims;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Server.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Ignore auth requests
        if (context.Request.Path.StartsWithSegments("/api/auth")
            || context.Request.Method == "GET") // Ignore GET requests
        {
            await _next(context);
            return;
        }
        
        var userName = context.User?.Claims.FirstOrDefault(c => c.Type == Constants.NAME)?.Value ?? "Unknown";

        var requestLog = new RequestLog
        {
            User = userName,
            Path = context.Request.Path,
            Method = context.Request.Method,
            Timestamp = DateTime.UtcNow
        };
        
        _logger.LogInformation("Request: {Method} {Path} by {User}", requestLog.Method, requestLog.Path, requestLog.User);
        
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SofteamDbContext>();
            dbContext.RequestLogs.Add(requestLog);
            await dbContext.SaveChangesAsync();
        }

        await _next(context);
    }
}