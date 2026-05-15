using Microsoft.AspNetCore.Mvc;
using AnalyticsHub.Service.Core;

namespace AnalyticsHub.Service.Controllers;

[ApiController]
[Route("analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsCore _core = new();

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        var result = await _core.Latest();
        return Ok(result);
    }

    [HttpPost("collect")]
    public async Task<IActionResult> Collect()
    {
        var result = await _core.Collect();
        return Ok(result);
    }
}