using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Server.Models.DTOs;
using SofteamDashboard.Server.Models.Requests;

namespace SofteamDashboard.Server.Endpoints.Logs;

public class GetLogs : Endpoint<GetLogsRequest, IEnumerable<RequestLogDTO>>
{
    private readonly SofteamDbContext _context;
    
    public GetLogs(SofteamDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/logs");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetLogsRequest req, CancellationToken ct)
    {
        var query = _context.RequestLogs.AsQueryable();
        
        if (req.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == req.UserId);
        }
        
        var logs = await query
            .OrderByDescending(x => x.Timestamp)
            .Skip(req.Page * req.PageSize)
            .Take(req.PageSize)
            .Select(x => new RequestLogDTO
            {
                Id = x.Id,
                User = x.UserId.ToString(),
                Path = x.Path,
                Method = x.Method,
                Timestamp = x.Timestamp,
                Body = x.Body
            })
            .ToListAsync(ct);

        await SendOkAsync(logs, ct);
    }
}