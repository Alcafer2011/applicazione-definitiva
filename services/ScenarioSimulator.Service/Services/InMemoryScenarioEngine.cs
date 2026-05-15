using AIEnterpriseOS.ScenarioSimulator.Service.Models;

namespace AIEnterpriseOS.ScenarioSimulator.Service.Services;

public interface IScenarioEngine
{
    ScenarioResult Simulate(ScenarioRequest request);
    ScenarioResult? GetResult(string id);
}

public class InMemoryScenarioEngine : IScenarioEngine
{
    private readonly List<ScenarioResult> _results = new();

    public ScenarioResult Simulate(ScenarioRequest request)
    {
        var result = new ScenarioResult
        {
            ScenarioRequestId = request.Id,
            ImpactRevenue = request.Percentage * 1000, // placeholder logic
            ImpactCosts = request.Percentage * 500,
            ImpactProfit = request.Percentage * 500,
            Summary = $"Scenario {request.Type} applied with {request.Percentage}% change."
        };

        _results.Add(result);
        return result;
    }

    public ScenarioResult? GetResult(string id)
        => _results.FirstOrDefault(x => x.Id == id);
}
