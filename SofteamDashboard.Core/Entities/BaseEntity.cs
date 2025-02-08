using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Status Status { get; set; } = Status.Ativo;
}