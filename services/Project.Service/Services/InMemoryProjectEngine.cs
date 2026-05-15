using AIEnterpriseOS.Project.Service.Models;

namespace AIEnterpriseOS.Project.Service.Services;

public interface IProjectEngine
{
    ProjectModel CreateProject(ProjectModel project);
    ProjectModel? GetProject(string id);
    IEnumerable<ProjectModel> GetAllProjects();

    ProjectTask AddTask(ProjectTask task);
    IEnumerable<ProjectTask> GetTasks(string projectId);
    ProjectTask? UpdateTaskStatus(string taskId, TaskStatus status);
}

public class InMemoryProjectEngine : IProjectEngine
{
    private readonly List<ProjectModel> _projects = new();
    private readonly List<ProjectTask> _tasks = new();

    public ProjectModel CreateProject(ProjectModel project)
    {
        _projects.Add(project);
        return project;
    }

    public ProjectModel? GetProject(string id)
        => _projects.FirstOrDefault(x => x.Id == id);

    public IEnumerable<ProjectModel> GetAllProjects()
        => _projects;

    public ProjectTask AddTask(ProjectTask task)
    {
        _tasks.Add(task);

        var project = _projects.FirstOrDefault(x => x.Id == task.ProjectId);
        if (project != null)
            project.Tasks.Add(task);

        return task;
    }

    public IEnumerable<ProjectTask> GetTasks(string projectId)
        => _tasks.Where(x => x.ProjectId == projectId);

    public ProjectTask? UpdateTaskStatus(string taskId, TaskStatus status)
    {
        var task = _tasks.FirstOrDefault(x => x.Id == taskId);
        if (task != null)
            task.Status = status;

        return task;
    }
}
