using Microsoft.AspNetCore.Mvc;
using AdminConsole.Service.Models;
using AdminConsole.Service.Core;

namespace AdminConsole.Service.Controllers;

[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    private readonly AdminCore _core = new();

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(AdminUser u)
    {
        return Ok(await _core.CreateUser(u));
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(AdminRole r)
    {
        return Ok(await _core.CreateRole(r));
    }

    [HttpPost("licenses")]
    public async Task<IActionResult> AddLicense(LicenseInfo l)
    {
        return Ok(await _core.AddLicense(l));
    }

    [HttpPost("airules")]
    public async Task<IActionResult> AddAiRule(AiRule rule)
    {
        return Ok(await _core.AddAiRule(rule));
    }

    [HttpPost("services/restart/{name}")]
    public async Task<IActionResult> Restart(string name)
    {
        await _core.RestartService(name);
        return Ok(new { status = "restarted", service = name });
    }
}