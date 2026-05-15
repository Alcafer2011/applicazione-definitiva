namespace AIEnterpriseOS.ActionAI.Service.Models;

public enum ActionStatus
{
    Pending,
    Running,
    Completed,
    Failed
}

public class ActionRequest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Intent { get; set; } = string.Empty;
    public string? ContextJson { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ActionExecution
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ActionRequestId { get; set; } = string.Empty;
    public ActionStatus Status { get; set; } = ActionStatus.Pending;
    public string? Log { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}

public class ActionDefinition
{
    public string Code { get; set; } = string.Empty; // es: create_invoice, plan_load_3d
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
