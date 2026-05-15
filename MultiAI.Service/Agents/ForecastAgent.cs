using MultiAI.Service.Models;

namespace MultiAI.Service.Core;

public class ForecastAgent : IAgent
{
    public string Name => "ForecastAgent";

    public bool CanHandle(string goal) =>
        goal.Contains("forecast", StringComparison.OrdinalIgnoreCase) ||
        goal.Contains("previsione", StringComparison.OrdinalIgnoreCase);

    public string Execute(AIRequest request)
    {
        return "Previsione: crescita stimata +12% nel prossimo trimestre.";
    }
}