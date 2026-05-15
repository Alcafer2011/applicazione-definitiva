using MultiAI.Service.Models;

namespace MultiAI.Service.Core;

public class InsightAgent : IAgent
{
    public string Name => "InsightAgent";

    public bool CanHandle(string goal) =>
        goal.Contains("insight", StringComparison.OrdinalIgnoreCase) ||
        goal.Contains("analisi", StringComparison.OrdinalIgnoreCase);

    public string Execute(AIRequest request)
    {
        return "Analisi completata: trend positivo negli ultimi 30 giorni.";
    }
}