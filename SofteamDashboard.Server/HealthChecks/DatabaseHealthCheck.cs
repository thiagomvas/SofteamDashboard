using Microsoft.Extensions.Diagnostics.HealthChecks;
using SofteamDashboard.Core;

namespace SofteamDashboard.Server.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly SofteamDbContext _context;
    
    public DatabaseHealthCheck(SofteamDbContext context)
    {
        _context = context;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
            return canConnect 
                ? HealthCheckResult.Healthy("Database is reachable.") 
                : HealthCheckResult.Unhealthy("Database connection failed.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database connection error.", ex);
        }
    }
}