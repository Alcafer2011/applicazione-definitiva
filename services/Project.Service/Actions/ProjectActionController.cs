using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.Project.Service.Models;
using AIEnterpriseOS.Project.Service.Scheduling;
using AIEnterpriseOS.Project.Service.Services;

namespace AIEnterpriseOS.Project.Service.Actions;

[ApiController]
[Route("api/project/action")]
public class ProjectActionController : ControllerBase
{
    private readonly ISchedulingEngine _scheduler;
    private readonly IProjectEngine _engine;

    public ProjectActionController(ISchedulingEngine scheduler, IProjectEngine engine)
    {
        _scheduler = scheduler;
        _engine = engine;
    }

    [HttpPost("estimate-duration")]
    public IActionResult EstimateDuration([FromBody] ProjectTask task)
    {
        var hours = _scheduler.EstimateDuration(task.Title, task.Title);
        return Ok(new { estimatedHours = hours });
    }

    [HttpPost("auto-schedule/{taskId}")]
    public IActionResult AutoSchedule(string taskId)
    {
        var task = _engine.GetTasks(taskId).FirstOrDefault();
        if (task is null) return NotFound();

        var end = _scheduler.AutoSchedule(DateTime.UtcNow, task.EstimatedHours);
        task.DueDate = end;

        return Ok(new { message = "Task scheduled", dueDate = end });
    }
}
