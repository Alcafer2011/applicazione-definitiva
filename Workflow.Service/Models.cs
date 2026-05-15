namespace Workflow.Service.Models;

public class WorkflowDefinition
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public List<string> States { get; set; } = new();
    public List<WorkflowTransition> Transitions { get; set; } = new();
}

public class WorkflowTransition
{
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public string Trigger { get; set; } = "";
}

public class WorkflowInstance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DefinitionId { get; set; } = "";
    public string CurrentState { get; set; } = "";
}