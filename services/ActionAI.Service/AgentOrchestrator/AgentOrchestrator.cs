namespace AIEnterpriseOS.ActionAI.Service.AgentOrchestrator;

public interface IAgent
{
    Task<string> ExecuteAsync(Dictionary<string, string> context);
}

public class FinanceAgent : IAgent
{
    public async Task<string> ExecuteAsync(Dictionary<string, string> context)
    {
        await Task.Delay(200);
        return "FinanceAgent executed action: " + context["intent"];
    }
}

public class CustomerAgent : IAgent
{
    public async Task<string> ExecuteAsync(Dictionary<string, string> context)
    {
        await Task.Delay(200);
        return "CustomerAgent executed action: " + context["intent"];
    }
}

public interface IAgentOrchestrator
{
    Task<string> DispatchAsync(string module, Dictionary<string, string> context);
}

public class AgentOrchestrator : IAgentOrchestrator
{
    private readonly FinanceAgent _finance = new();
    private readonly CustomerAgent _customer = new();

    public async Task<string> DispatchAsync(string module, Dictionary<string, string> context)
    {
        return module switch
        {
            "Finance.Service" => await _finance.ExecuteAsync(context),
            "CustomerHub.Service" => await _customer.ExecuteAsync(context),
            _ => "No agent available for module: " + module
        };
    }
}
