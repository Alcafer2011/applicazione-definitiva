namespace DigitalBrain.Service.Models;

public class MemoryRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TenantId { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}