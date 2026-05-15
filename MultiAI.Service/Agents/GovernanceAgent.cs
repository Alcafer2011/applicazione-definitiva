using MultiAI.Service.Models;

namespace MultiAI.Service.Core;

public class GovernanceAgent : IAgent
{
    public string Name => "GovernanceAgent";

    public bool CanHandle(string goal) =>
        goal.Contains("policy", StringComparison.OrdinalIgnoreCase) ||
        goal.Contains("regola", StringComparison.OrdinalIgnoreCase);

    public string Execute(AIRequest request)
    {
        return "Regola applicata: workflow approvato automaticamente.";
    }
}