namespace AIEnterpriseOS.Manufacturing.Service.Models;

public class BomItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Component { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
}

public class BillOfMaterials
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Product { get; set; } = string.Empty;
    public List<BomItem> Items { get; set; } = new();
}

public class RoutingStep
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WorkCenter { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
}

public class Routing
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Product { get; set; } = string.Empty;
    public List<RoutingStep> Steps { get; set; } = new();
}

public enum WorkOrderStatus
{
    Planned,
    InProgress,
    Completed
}

public class WorkOrder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Planned;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
