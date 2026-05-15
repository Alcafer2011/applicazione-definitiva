namespace AIEnterpriseOS.ActionAI.Service.WorkflowExecutor;

public class WorkflowStep
{
    public string Name { get; set; } = string.Empty;
    public Func<Task<string>> Action { get; set; } = default!;
}

public interface IWorkflowExecutor
{
    Task<List<string>> ExecuteAsync(List<WorkflowStep> steps);
}

public class WorkflowExecutor : IWorkflowExecutor
{
    public async Task<List<string>> ExecuteAsync(List<WorkflowStep> steps)
    {
        var results = new List<string>();

        foreach (var step in steps)
        {
            try
            {
                var result = await step.Action();
                results.Add($"Step {step.Name}: {result}");
            }
            catch (Exception ex)
            {
                results.Add($"Step {step.Name} FAILED: {ex.Message}");
                break;
            }
        }

        return results;
    }
}
