namespace HrHarmony.Data.Models.Entities;

public class Visitor : Entity<int>
{
    public string VisitorId { get; set; }

    public DateTime Timestamp { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? Path { get; set; }
}
