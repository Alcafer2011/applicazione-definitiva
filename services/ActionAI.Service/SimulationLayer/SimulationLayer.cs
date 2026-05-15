namespace AIEnterpriseOS.ActionAI.Service.SimulationLayer;

public interface ISimulationLayer
{
    string Simulate(string intent);
}

public class BasicSimulationLayer : ISimulationLayer
{
    public string Simulate(string intent)
    {
        return $"Simulation: executing '{intent}' has low risk.";
    }
}
