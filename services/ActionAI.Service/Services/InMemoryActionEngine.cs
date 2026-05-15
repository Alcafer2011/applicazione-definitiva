using AIEnterpriseOS.ActionAI.Service.Models;

namespace AIEnterpriseOS.ActionAI.Service.Services;

public interface IActionEngine
{
    Task<ActionExecution> ExecuteAsync(ActionRequest request);
    IEnumerable<ActionDefinition> GetCatalog();
    ActionExecution? GetStatus(string executionId);
}

public class InMemoryActionEngine : IActionEngine
{
    private readonly List<ActionExecution> _executions = new();
    private readonly List<ActionDefinition> _catalog = new()
    {
        new ActionDefinition
        {
            Code = "create_invoice",
            Name = "Create Invoice",
            Description = "Create an invoice for a given customer and order."
        },
        new ActionDefinition
        {
            Code = "plan_load_3d",
            Name = "Plan 3D Load",
            Description = "Generate a 3D load plan for logistics."
        }
    };

    public IEnumerable<ActionDefinition> GetCatalog() => _catalog;

    public ActionExecution? GetStatus(string executionId)
        => _executions.FirstOrDefault(x => x.Id == executionId);

    public async Task<ActionExecution> ExecuteAsync(ActionRequest request)
    {
        var exec = new ActionExecution
        {
            ActionRequestId = request.Id,
            Status = ActionStatus.Running,
            Log = "Execution started"
        };

        _executions.Add(exec);

        // TODO: here we will call MultiAI + Workflow + other services
        await Task.Delay(500); // simulate work

        exec.Status = ActionStatus.Completed;
        exec.CompletedAt = DateTime.UtcNow;
        exec.Log = "Execution completed (stub).";

        return exec;
    }
}
