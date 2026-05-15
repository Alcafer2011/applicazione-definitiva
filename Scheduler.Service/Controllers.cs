using Microsoft.AspNetCore.Mvc;
using Scheduler.Service.Models;
using Scheduler.Service.Core;

namespace Scheduler.Service.Controllers;

[ApiController]
[Route("scheduler")]
public class SchedulerController : ControllerBase
{
    private readonly SchedulerCore _core = new();

    [HttpGet("tasks")]
    public async Task<IActionResult> GetTasks()
    {
        return Ok(await _core.GetTasks());
    }

    [HttpPost("tasks")]
    public async Task<IActionResult> AddTask(ScheduledTask task)
    {
        return Ok(await _core.AddTask(task));
    }
}