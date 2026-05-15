using MultiAI.Service.Models;

namespace MultiAI.Service.Core;

public class BInchenninnso
{
    private readonly List<IAgent> _agents;

    public BInchenninnso(List<IAgent> agents)
    {
        _agents = agents;
    }

    public AIResponse Process(AIRequest request)
    {
        // Seleziona l'agente migliore
        var agent = _agents.FirstOrDefault(a => a.CanHandle(request.Goal));
        if (agent == null)
        {
            return new AIResponse
            {
                Agent = "None",
                Result = "Nessun agente disponibile per questo obiettivo."
            };
        }

        // Esegue l'agente
        var result = agent.Execute(request);

        return new AIResponse
        {
            Agent = agent.Name,
            Result = result
        };
    }
}