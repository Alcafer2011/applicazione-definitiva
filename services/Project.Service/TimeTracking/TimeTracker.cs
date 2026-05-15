namespace AIEnterpriseOS.Project.Service.TimeTracking;

public interface ITimeTracker
{
    void LogHours(string taskId, int hours);
}

public class InMemoryTimeTracker : ITimeTracker
{
    private readonly List<ProjectTask> _tasks;

    public InMemoryTimeTracker(List<ProjectTask> tasks)
    {
        _tasks = tasks;
    }

    public void LogHours(string taskId, int hours)
    {
        var t = _tasks.First(x => x.Id == taskId);
        t.LoggedHours += hours;
    }
}
