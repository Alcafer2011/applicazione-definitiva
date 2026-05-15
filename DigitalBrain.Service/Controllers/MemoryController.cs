using DigitalBrain.Service.Abstractions;
using DigitalBrain.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBrain.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemoryController : ControllerBase
{
    private readonly IMemoryStore _store;

    public MemoryController(IMemoryStore store)
    {
        _store = store;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MemoryRecord record, CancellationToken ct)
    {
        await _store.AddAsync(record, ct);
        return Ok(record);
    }

    [HttpGet]
    public async Task<IActionResult> Query([FromQuery] string tenantId, [FromQuery] string? type, CancellationToken ct)
    {
        var results = await _store.QueryAsync(tenantId, type, ct);
        return Ok(results);
    }
}