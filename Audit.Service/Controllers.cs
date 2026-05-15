using Microsoft.AspNetCore.Mvc;
using Audit.Service.Models;
using Audit.Service.Core;

namespace Audit.Service.Controllers;

[ApiController]
[Route("audit")]
public class AuditController : ControllerBase
{
    private readonly AuditCore _core = new();

    [HttpPost("log")]
    public async Task<IActionResult> Log(AuditEntry entry)
    {
        await _core.AddEntry(entry);
        return Ok(new { status = "logged" });
    }

    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        var result = await _core.History();
        return Ok(result);
    }

    [HttpPost("rules")]
    public async Task<IActionResult> AddRule(ComplianceRule rule)
    {
        var result = await _core.AddRule(rule);
        return Ok(result);
    }
}