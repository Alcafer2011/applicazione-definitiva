namespace AIEnterpriseOS.AIMemory.Service.Models;

public enum MemoryType
{
    Decision,
    Preference,
    Pattern,
    Error
}

public class MemoryRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TenantId { get; set; } = string.Empty;
    public MemoryType Type { get; set; }
    public string Content { get; set; } = string.Empty;
    public double Score { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
