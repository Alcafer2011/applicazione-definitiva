namespace AIEnterpriseOS.ScenarioSimulator.Service.Models;

public enum ScenarioType
{
    PriceChange,
    CostChange,
    StaffChange,
    StockChange,
    LogisticsChange
}

public class ScenarioRequest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public ScenarioType Type { get; set; }
    public decimal Percentage { get; set; }
    public string? ContextJson { get; set; }
}

public class ScenarioResult
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ScenarioRequestId { get; set; } = string.Empty;
    public decimal ImpactRevenue { get; set; }
    public decimal ImpactCosts { get; set; }
    public decimal ImpactProfit { get; set; }
    public string Summary { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
