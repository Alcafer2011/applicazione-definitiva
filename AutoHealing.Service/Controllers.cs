using Microsoft.AspNetCore.Mvc;
using AutoHealing.Service.Core;

namespace AutoHealing.Service.Controllers;

[ApiController]
[Route("autohealing")]
public class AutoHealingController : ControllerBase
{
    private readonly AutoHealingCore _core = new();

    [HttpGet("status")]
    public async Task<IActionResult> Status()
    {
        var result = await _core.CheckAllAsync();
        return Ok(result);
    }

    [HttpPost("restart/{name}")]
    public async Task<IActionResult> Restart(string name)
    {
        var containers = await _core.CheckAllAsync();
        var target = containers.FirstOrDefault(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (target == null)
            return NotFound("Servizio non trovato");

        await _core.RestartAsync(target.Name);
        return Ok($"Riavviato: {target.Name}");
    }
}