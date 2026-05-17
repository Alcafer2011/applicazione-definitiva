using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Project.Service.Models;
using AIEnterpriseOS.Project.Service.Services;

namespace AIEnterpriseOS.Project.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectEngine _engine;

    public ProjectController(IProjectEngine engine)
    {
        _engine = engine;
    }

    [HttpPost]
    public IActionResult CreateProject([FromBody] ProjectModel project)
    {
        return Ok(_engine.CreateProject(project));
    }

    [HttpGet("{id}")]
    public IActionResult GetProject(string id)
    {
        var p = _engine.GetProject(id);
        if (p is null) return NotFound();
        return Ok(p);
    }

    [HttpGet]
    public IActionResult GetAllProjects()
    {
        return Ok(_engine.GetAllProjects());
    }

    [HttpPost("task")]
    public IActionResult AddTask([FromBody] ProjectTask task)
    {
        return Ok(_engine.AddTask(task));
    }

    [HttpGet("tasks/{projectId}")]
    public IActionResult GetTasks(string projectId)
    {
        return Ok(_engine.GetTasks(projectId));
    }

    [HttpPost("task/{taskId}/status/{status}")]
    public IActionResult UpdateTaskStatus(string taskId, ProjectTaskStatus status)
    {
        var t = _engine.UpdateTaskStatus(taskId, status);
        if (t is null) return NotFound();
        return Ok(t);
    }
}
