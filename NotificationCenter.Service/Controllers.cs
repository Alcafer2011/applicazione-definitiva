using Microsoft.AspNetCore.Mvc;
using NotificationCenter.Service.Models;
using NotificationCenter.Service.Core;

namespace NotificationCenter.Service.Controllers;

[ApiController]
[Route("notifications")]
public class NotificationController : ControllerBase
{
    private readonly NotificationCore _core = new();

    [HttpPost("publish")]
    public async Task<IActionResult> Publish(NotificationEvent evt)
    {
        await _core.Publish(evt);
        return Ok(new { status = "published" });
    }

    [HttpPost("webhooks")]
    public async Task<IActionResult> AddWebhook(WebhookTarget hook)
    {
        var result = await _core.AddWebhook(hook);
        return Ok(result);
    }

    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        var result = await _core.History();
        return Ok(result);
    }
}