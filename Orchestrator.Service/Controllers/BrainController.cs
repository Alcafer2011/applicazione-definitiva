using Microsoft.AspNetCore.Mvc;
using Orchestrator.Service.Services;

namespace Orchestrator.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrainController : ControllerBase
{
    private readonly IOrchestratorRouter _router;

    public BrainController(IOrchestratorRouter router)
    {
        _router = router;
    }

    public record BrainInput(string TenantId, string Question);

    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] BrainInput input, CancellationToken ct)
    {
        var payload = new {
            TenantId = input.TenantId,
            Question = input.Question
        };

        var result = await _router.RouteAsync(
            new("brain", "reason", payload),
            ct);

        return Ok(result);
    }
}