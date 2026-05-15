using Microsoft.AspNetCore.Mvc;
using Message.Service.Models;
using Message.Service.Core;

namespace Message.Service.Controllers;

[ApiController]
[Route("bus")]
public class BusController : ControllerBase
{
    private readonly MessageBus _bus = new();

    [HttpPost("publish")]
    public async Task<IActionResult> Publish(BusMessage msg)
    {
        await _bus.Publish(msg);
        return Ok(new { status = "published" });
    }
}