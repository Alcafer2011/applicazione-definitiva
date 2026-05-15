namespace AIEnterpriseOS.Project.Service.Models;

public enum TaskStatus
{
    Todo,
    InProgress,
    Blocked,
    Completed
}

public class ProjectModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
public DateTime? StartDate { get; set; }
public int EstimatedHours { get; set; }
public int LoggedHours { get; set; }
public decimal HourlyCost { get; set; }
public decimal TaskCost => HourlyCost * LoggedHours;
public string? DependsOnTaskId { get; set; }
    public List<ProjectTask> Tasks { get; set; } = new();
public decimal TotalCost => Tasks.Sum(t => t.TaskCost);
public double Progress => Tasks.Count == 0 ? 0 : Tasks.Count(t => t.Status == TaskStatus.Completed) * 100.0 / Tasks.Count;
}

public class ProjectTask
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty; // HR employee ID
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
public DateTime? StartDate { get; set; }
public int EstimatedHours { get; set; }
public int LoggedHours { get; set; }
public decimal HourlyCost { get; set; }
public decimal TaskCost => HourlyCost * LoggedHours;
public string? DependsOnTaskId { get; set; }
    public string? DependsOnTaskId { get; set; }
}
