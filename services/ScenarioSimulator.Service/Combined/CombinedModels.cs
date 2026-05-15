namespace AIEnterpriseOS.ScenarioSimulator.Service.Models;

public class ScenarioVariable
{
    public string Name { get; set; } = string.Empty; // price, cost, staff, stock
    public decimal Percentage { get; set; }
}

public class CombinedScenarioRequest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public List<ScenarioVariable> Variables { get; set; } = new();
    public string? ContextJson { get; set; }
}

public class ScenarioBreakdown
{
    public decimal FinanceImpact { get; set; }
    public decimal ProjectImpact { get; set; }
    public decimal WarehouseImpact { get; set; }
    public decimal HRImpact { get; set; }
}

public class ScenarioComparison
{
    public string ScenarioA { get; set; } = string.Empty;
    public string ScenarioB { get; set; } = string.Empty;
    public decimal RevenueDiff { get; set; }
    public decimal CostDiff { get; set; }
    public decimal ProfitDiff { get; set; }
}
