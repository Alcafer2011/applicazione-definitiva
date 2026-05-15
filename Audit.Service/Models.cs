namespace Audit.Service.Models;

public class AuditEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Service { get; set; } = "";
    public string Action { get; set; } = "";
    public string User { get; set; } = "";
    public string Result { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public Dictionary<string, object> Data { get; set; } = new();
}

public class ComplianceRule
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Condition { get; set; } = "";
    public string Severity { get; set; } = "low";
}