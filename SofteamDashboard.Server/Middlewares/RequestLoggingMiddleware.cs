using System.Security.Claims;
using System.Text;
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
        
        var userId = int.Parse(context.User?.Claims.FirstOrDefault(c => c.Type == Constants.USERID)?.Value ?? "0");

        var requestLog = new RequestLog
        {
            UserId = userId,
            Path = context.Request.Path,
            Method = context.Request.Method,
            Timestamp = DateTime.UtcNow,
        };
        
        var requestContent = "";
        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            requestContent = await reader.ReadToEndAsync();
        }
        context.Request.Body.Position = 0;
        requestLog.Body = requestContent;
        
        
        _logger.LogInformation("Request: {Method} {Path} with User ID '{UserId}'", requestLog.Method, requestLog.Path, requestLog.UserId);
        
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SofteamDbContext>();
            dbContext.RequestLogs.Add(requestLog);
            await dbContext.SaveChangesAsync();
        }

        await _next(context);
    }
}