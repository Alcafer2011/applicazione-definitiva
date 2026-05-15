namespace Twin.Service.Models;

public class SystemAlert
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string NodeId { get; set; } = default!;
    public string Severity { get; set; } = "Info";
    public string Message { get; set; } = default!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}