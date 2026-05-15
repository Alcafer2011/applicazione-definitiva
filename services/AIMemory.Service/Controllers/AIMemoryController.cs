using Microsoft.AspNetCore.Mvc;
using AIEnterpriseOS.AIMemory.Service.Models;
using AIEnterpriseOS.AIMemory.Service.Services;

namespace AIEnterpriseOS.AIMemory.Service.Controllers;

[ApiController]
[Route("api/aimemory")]
public class AIMemoryController : ControllerBase
{
    private readonly IMemoryEngine _engine;

    public AIMemoryController(IMemoryEngine engine)
    {
        _engine = engine;
    }

    [HttpPost("save")]
    public IActionResult Save([FromBody] MemoryRecord record)
    {
        return Ok(_engine.Save(record));
    }

    [HttpGet("query")]
    public IActionResult Query(string tenantId, string q)
    {
        return Ok(_engine.Query(tenantId, q));
    }

    [HttpGet("all/{tenantId}")]
    public IActionResult All(string tenantId)
    {
        return Ok(_engine.GetAll(tenantId));
    }
}
