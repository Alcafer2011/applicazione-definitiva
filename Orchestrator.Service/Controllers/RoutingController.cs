using Microsoft.AspNetCore.Mvc;
using Orchestrator.Service.Models;
using Orchestrator.Service.Services;

namespace Orchestrator.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoutingController : ControllerBase
{
    private readonly IOrchestratorRouter _router;

    public RoutingController(IOrchestratorRouter router)
    {
        _router = router;
    }

    [HttpPost]
    public async Task<IActionResult> Route([FromBody] RouteRequest req, CancellationToken ct)
    {
        var result = await _router.RouteAsync(req, ct);
        return Ok(result);
    }
}