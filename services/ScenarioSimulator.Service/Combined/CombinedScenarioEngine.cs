using AIEnterpriseOS.ScenarioSimulator.Service.Models;

namespace AIEnterpriseOS.ScenarioSimulator.Service.Combined;

public interface ICombinedScenarioEngine
{
    ScenarioResult SimulateCombined(CombinedScenarioRequest request);
    ScenarioComparison Compare(ScenarioResult a, ScenarioResult b);
}

public class CombinedScenarioEngine : ICombinedScenarioEngine
{
    public ScenarioResult SimulateCombined(CombinedScenarioRequest request)
    {
        var totalImpact = request.Variables.Sum(v => v.Percentage * 1000);

        return new ScenarioResult
        {
            ScenarioRequestId = request.Id,
            ImpactRevenue = totalImpact,
            ImpactCosts = totalImpact * 0.6m,
            ImpactProfit = totalImpact * 0.4m,
            Summary = "Combined scenario executed with " + request.Variables.Count + " variables."
        };
    }

    public ScenarioComparison Compare(ScenarioResult a, ScenarioResult b)
    {
        return new ScenarioComparison
        {
            ScenarioA = a.Id,
            ScenarioB = b.Id,
            RevenueDiff = b.ImpactRevenue - a.ImpactRevenue,
            CostDiff = b.ImpactCosts - a.ImpactCosts,
            ProfitDiff = b.ImpactProfit - a.ImpactProfit
        };
    }
}
