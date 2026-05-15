using DigitalBrain.Service.Abstractions;
using DigitalBrain.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBrain.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReasoningController : ControllerBase
{
    private readonly IMemoryStore _memory;
    private readonly IReasoningEngine _engine;

    public ReasoningController(IMemoryStore memory, IReasoningEngine engine)
    {
        _memory = memory;
        _engine = engine;
    }

    public record ReasoningInput(string TenantId, string Question);

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReasoningInput input, CancellationToken ct)
    {
        var context = await _memory.QueryAsync(input.TenantId, null, ct);

        var result = await _engine.ReasonAsync(
            new ReasoningRequest(input.TenantId, input.Question, context),
            ct);

        return Ok(result);
    }
}